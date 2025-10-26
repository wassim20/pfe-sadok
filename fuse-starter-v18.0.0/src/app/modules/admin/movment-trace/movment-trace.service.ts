// src/app/modules/admin/apps/logistics/movement-traces/movment-trace.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { forkJoin, Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { SapService } from '../sap/sap.service';
import { ReturnService } from '../return-line/return.service';
import { BaseCompanyService } from 'app/core/services/base-company.service';
import { AuthService } from 'app/core/auth/auth.service';


@Injectable({
  providedIn: 'root'
})
export class MovementTraceService extends BaseCompanyService {
  protected apiUrl = 'http://localhost:5288/api/MovementTraces'; // Base URL de votre API MovementTraces
  // private sapUrl = 'http://localhost:5288/api/Sap'; // Pas nécessaire si on utilise SapService
  private returnLinesUrl = 'http://localhost:5288/api/ReturnLines'; // Base URL de votre API ReturnLines

  constructor(
    http: HttpClient,
    authService: AuthService,
    private _sapService: SapService, // Injecter le service Sap
    private _returnService: ReturnService // Injecter le service ReturnLine
  ) { 
    super(http, authService);
  }

  // 🏢 Company-aware methods (inherited from BaseCompanyService)
  // - getAllByCompany(isActive?: boolean): Observable<any[]>
  // - getByIdAndCompany(id: number): Observable<any>
  // - createForCompany(dto: any): Observable<any>
  // - updateForCompany(id: number, dto: any): Observable<any>
  // - setActiveStatusForCompany(id: number, value: boolean): Observable<any>

  // Legacy methods for backward compatibility
  // GET: /api/MovementTraces?isActive=true
  getAll(isActive: boolean | null = true): Observable<any[]> {
    return this.getAllByCompany(isActive); // 🏢 Use company-aware method
  }

  // GET: /api/MovementTraces/{id}
  getById(id: number): Observable<any> {
    return this.getByIdAndCompany(id); // 🏢 Use company-aware method
  }

  // POST: /api/MovementTraces
  create(dto: any): Observable<any> {
    return this.createForCompany(dto); // 🏢 Use company-aware method
  }

  // PUT: /api/MovementTraces/{id}/set-active?value=true
  setActiveStatus(id: number, value: boolean): Observable<any> {
    return this.setActiveStatusForCompany(id, value); // 🏢 Use company-aware method
  }

  // --- MÉTHODE PRINCIPALE POUR LE RETOUR ---
  /**
   * Traite le retour complet d'un MovementTrace :
   * 1. Crée un ReturnLine.
   * 2. Met à jour le stock Sap associé.
   * @param movementTraceData L'objet MovementTraceReadDto complet.
   * @param userId L'ID de l'utilisateur effectuant le retour.
   * @returns Un Observable qui émet un objet contenant les résultats de chaque appel.
   */
  processReturn(
    movementTraceData: any, // MovementTraceReadDto
    userId: number
  ): Observable<{ returnLineCreated: any; sapUpdated: any }> {
    console.log(`[MovementTraceService] Traitement du retour pour MovementTrace ID ${movementTraceData?.id}`);

    // 1. Vérifier les données d'entrée
    if (!movementTraceData || !movementTraceData.id) {
      console.error('[MovementTraceService] Données de MovementTrace invalides ou ID manquant.');
      return throwError(() => new Error('Données de MovementTrace invalides ou ID manquant.'));
    }
    if (!userId || userId <= 0) {
      console.error('[MovementTraceService] UserId invalide.');
      return throwError(() => new Error('UserId invalide.'));
    }

    // 2. Préparer les données pour ReturnLineCreateDto
    // Assurez-vous que les noms de propriétés correspondent à votre backend
    const returnLineData: any = {
      usCode: movementTraceData.usNom, // Utiliser le code US du MovementTrace
      quantite: movementTraceData.quantite, // Utiliser la quantité du MovementTrace
      articleId: movementTraceData.articleId || movementTraceData.detailPicklist?.articleId, // Obtenir ArticleId
      userId: userId, // ID de l'utilisateur passé en paramètre
      statusId: 1 // Statut initial, ex: 1 = "En Attente" (à ajuster selon votre logique)
    };

    // 3. Préparer les données pour SapUpdateDto
    // Vous devez déterminer comment obtenir le sapId ou l'usCode pour le mettre à jour.
    // Hypothèse : Utiliser l'usCode du MovementTrace pour identifier le SAP à mettre à jour.
    const usCodeToUpdate = movementTraceData.usNom;
    if (!usCodeToUpdate) {
      console.error('[MovementTraceService] usCode manquant dans les données du MovementTrace.');
      return throwError(() => new Error('usCode manquant dans les données du MovementTrace.'));
    }

    const sapUpdateData: any = {
      // Article: ??? // Le backend devrait savoir quel article mettre à jour via usCode
      usCode: usCodeToUpdate, // Code US à mettre à jour
      quantite: parseInt(movementTraceData.quantite, 10) || 0 // Quantité à ajouter (le backend doit l'ajouter)
    };

    // 4. Créer les observables pour les deux appels API
    // Appeler le service ReturnService pour créer le ReturnLine
    const createReturnLine$ = this._returnService.create(returnLineData); // <-- Utiliser ReturnService

    // Appeler le service SapService pour mettre à jour le stock
    // Hypothèse : Vous avez un endpoint dans SapController pour mettre à jour par usCode
    // Exemple : POST /api/Sap/add-stock
    const updateSap$ = this._sapService.addStock(sapUpdateData); // <-- Utiliser SapService.addStock

    // 5. Exécuter les deux appels en parallèle et combiner les résultats
    return forkJoin({
      returnLineCreated: createReturnLine$,
      sapUpdated: updateSap$
    }).pipe(
      catchError((err) => {
        console.error('[MovementTraceService] Erreur dans forkJoin (processReturn):', err);
        // Retransmettre l'erreur
        return throwError(err);
      })
    );
  }
  // --- FIN DE LA MÉTHODE PRINCIPALE ---
}
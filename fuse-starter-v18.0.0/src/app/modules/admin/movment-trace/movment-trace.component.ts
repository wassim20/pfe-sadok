// src/app/modules/admin/apps/logistics/movement-traces/movement-trace.component.ts
import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { Subject, takeUntil } from 'rxjs';
import { MovementTraceService } from './movment-trace.service'; // Vérifiez le nom du fichier
import { UserService } from 'app/core/user/user.service';
import { User } from 'app/core/user/user.types';

@Component({
  selector: 'app-movement-trace',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTableModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatDialogModule,
  ],
  template: `
    <div class="flex flex-col w-full h-full">
      <!-- Header -->
      <div class="relative flex flex-col sm:flex-row flex-0 sm:items-center sm:justify-between py-4 px-6 md:px-8 border-b">
        <div class="text-3xl font-bold tracking-tight">Traçabilité des Mouvements</div>
        <div class="flex flex-shrink-0 items-center mt-4 sm:mt-0 sm:ml-4">
          <button
            mat-stroked-button
            [color]="'accent'"
            (click)="toggleActiveFilter()"
            class="ml-4">
            <mat-icon [svgIcon]="'heroicons_outline:eye'"></mat-icon>
            <span class="ml-2">Afficher {{ isActiveFilter ? 'Inactifs' : 'Actifs' }}</span>
          </button>
        </div>
      </div>

      <!-- Main Content -->
      <div class="flex-auto overflow-y-auto p-6 md:p-8">
        <!-- Loading Spinner -->
        <div class="flex flex-col items-center justify-center h-full" *ngIf="loading">
          <mat-spinner diameter="40"></mat-spinner>
          <div class="mt-4 text-secondary">Chargement des mouvements...</div>
        </div>

        <!-- MovementTraces Table/List -->
        <ng-container *ngIf="!loading">
          <mat-card class="shadow-md rounded-lg overflow-hidden">
            <mat-card-header class="bg-gray-100 rounded-t-lg">
              <mat-card-title class="text-xl text-gray-800">Liste des Mouvements</mat-card-title>
            </mat-card-header>
            <mat-card-content class="p-0">
              <div class="overflow-x-auto">
                <table mat-table [dataSource]="movementTraces" class="min-w-full">
                  <!-- ID Column -->
                  <ng-container matColumnDef="id">
                    <th mat-header-cell *matHeaderCellDef class="px-4 py-3 bg-gray-50 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">ID</th>
                    <td mat-cell *matCellDef="let element" class="px-4 py-2 whitespace-nowrap text-sm text-gray-900">{{ element.id }}</td>
                  </ng-container>

                  <!-- UsNom Column -->
                  <ng-container matColumnDef="usNom">
                    <th mat-header-cell *matHeaderCellDef class="px-4 py-3 bg-gray-50 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Code US</th>
                    <td mat-cell *matCellDef="let element" class="px-4 py-2 whitespace-nowrap text-sm text-gray-900">{{ element.usNom }}</td>
                  </ng-container>

                  <!-- DateMouvement Column -->
                  <ng-container matColumnDef="dateMouvement">
                    <th mat-header-cell *matHeaderCellDef class="px-4 py-3 bg-gray-50 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Date Mouvement</th>
                    <td mat-cell *matCellDef="let element" class="px-4 py-2 whitespace-nowrap text-sm text-gray-900">{{ element.dateMouvement | date:'short' }}</td>
                  </ng-container>

                  <!-- Quantite Column -->
                  <ng-container matColumnDef="quantite">
                    <th mat-header-cell *matHeaderCellDef class="px-4 py-3 bg-gray-50 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Quantité</th>
                    <td mat-cell *matCellDef="let element" class="px-4 py-2 whitespace-nowrap text-sm text-gray-900">{{ element.quantite }}</td>
                  </ng-container>

                  <!-- UserId Column -->
                  <ng-container matColumnDef="userId">
                    <th mat-header-cell *matHeaderCellDef class="px-4 py-3 bg-gray-50 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">ID Utilisateur</th>
                    <td mat-cell *matCellDef="let element" class="px-4 py-2 whitespace-nowrap text-sm text-gray-900">{{ element.userId }}</td>
                  </ng-container>

                  <!-- DetailPicklistId Column -->
                  <ng-container matColumnDef="detailPicklistId">
                    <th mat-header-cell *matHeaderCellDef class="px-4 py-3 bg-gray-50 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">ID Détail Picklist</th>
                    <td mat-cell *matCellDef="let element" class="px-4 py-2 whitespace-nowrap text-sm text-gray-900">{{ element.detailPicklistId }}</td>
                  </ng-container>

                  <!-- IsActive Column -->
                  <ng-container matColumnDef="isActive">
                    <th mat-header-cell *matHeaderCellDef class="px-4 py-3 bg-gray-50 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Statut</th>
                    <td mat-cell *matCellDef="let element" class="px-4 py-2 whitespace-nowrap text-sm">
                      <span [ngClass]="{
                        'px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-green-100 text-green-800': element.isActive,
                        'px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-red-100 text-red-800': !element.isActive
                      }">
                        {{ element.isActive ? 'Actif' : 'Inactif' }}
                      </span>
                    </td>
                  </ng-container>

                  <!-- Actions Column -->
                  <ng-container matColumnDef="actions">
                    <th mat-header-cell *matHeaderCellDef class="px-4 py-3 bg-gray-50 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">Actions</th>
                    <td mat-cell *matCellDef="let element" class="px-4 py-2 whitespace-nowrap text-right text-sm font-medium">
                      <button mat-icon-button [color]="'primary'" (click)="onView(element)" title="Voir Détails">
                        <mat-icon>visibility</mat-icon>
                      </button>
                      <button mat-icon-button [color]="'accent'" (click)="onEdit(element)" title="Modifier">
                        <mat-icon>edit</mat-icon>
                      </button>
                      <!-- --- MISE À JOUR : Passer l'objet complet --- -->
                      <button mat-icon-button [color]="'warn'" (click)="onReturn(element)" title="Retourner">
                        <mat-icon>undo</mat-icon> <!-- Ou 'reply', 'keyboard_return', etc. -->
                      </button>
                      <!-- --- FIN DE LA MISE À JOUR --- -->
                      <button mat-icon-button [color]="element.isActive ? 'warn' : 'primary'" (click)="onToggleActive(element.id, element.isActive)" title="{{ element.isActive ? 'Désactiver' : 'Activer' }}">
                        <mat-icon>{{ element.isActive ? 'toggle_off' : 'toggle_on' }}</mat-icon>
                      </button>
                    </td>
                  </ng-container>
                  <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
                  <tr mat-row *matRowDef="let row; columns: displayedColumns;" class="hover:bg-gray-50 transition-colors duration-150 ease-in-out"></tr>
                </table>
              </div>

              <!-- Empty State -->
              <div class="flex flex-col items-center justify-center p-8 text-gray-500" *ngIf="movementTraces.length === 0 && !loading">
                <mat-icon [svgIcon]="'heroicons_outline:inbox'" class="text-6xl block mx-auto mb-4"></mat-icon>
                <p class="text-lg">Aucun mouvement trouvé.</p>
                <p class="mt-2 text-center max-w-64">Commencez par créer des mouvements via les picklists.</p>
              </div>
            </mat-card-content>
          </mat-card>
        </ng-container>
      </div>
    </div>
  `,
  styles: [`
    :host {
      display: flex;
      flex-direction: column;
      height: 100%;
    }
    .mat-mdc-form-field {
      width: 100%;
    }
    mat-progress-spinner {
      display: inline-block;
      vertical-align: middle;
    }
  `]
})
export class MovementTraceComponent implements OnInit, OnDestroy {
  movementTraces: any[] = [];
  loading: boolean = false;
  isActiveFilter: boolean = true; // Default filter
  displayedColumns: string[] = ['id', 'usNom', 'dateMouvement', 'quantite', 'userId', 'detailPicklistId', 'isActive', 'actions'];
  private destroy$ = new Subject<void>();
  private _unsubscribeAll: Subject<any> = new Subject<any>();
  currentUser: User | null = null; // Stocker l'utilisateur connecté

  constructor(
    private _userService: UserService, // Corriger le nom du service
    private _movementTraceService: MovementTraceService,
    private _snackBar: MatSnackBar,
    private _dialog: MatDialog
  ) { }

  ngOnInit(): void {
    // Charger l'utilisateur courant
    this._userService.user$
      .pipe(takeUntil(this.destroy$))
      .subscribe((user: User | null) => {
        this.currentUser = user;
        console.log('[MovementTraceComponent] Utilisateur courant:', this.currentUser);
      });

    this.loadMovementTraces();
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadMovementTraces(): void {
    this.loading = true;
    this._movementTraceService.getAll(this.isActiveFilter)
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: (data) => {
          this.movementTraces = data || [];
          this.loading = false;
        },
        error: (err) => {
          console.error('Erreur lors du chargement des mouvements:', err);
          this._snackBar.open('Erreur lors du chargement des mouvements.', 'Erreur', { duration: 5000 });
          this.loading = false;
          this.movementTraces = [];
        }
      });
  }

  toggleActiveFilter(): void {
    this.isActiveFilter = !this.isActiveFilter;
    this.loadMovementTraces();
  }

  onView(movementTrace: any): void {
    console.log('Voir les détails du mouvement:', movementTrace);
    this._snackBar.open(`Affichage des détails du mouvement ID ${movementTrace.id}`, 'Info', { duration: 3000 });
  }

  onEdit(movementTrace: any): void {
    console.log('Modifier le mouvement:', movementTrace);
    this._snackBar.open(`Modification du mouvement ID ${movementTrace.id}`, 'Info', { duration: 3000 });
  }

  onToggleActive(id: number, currentStatus: boolean): void {
    this._movementTraceService.setActiveStatus(id, !currentStatus)
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: () => {
          const index = this.movementTraces.findIndex(mt => mt.id === id);
          if (index !== -1) {
            this.movementTraces[index].isActive = !currentStatus;
          }
          this._snackBar.open(`Statut du mouvement ID ${id} mis à jour.`, 'Succès', { duration: 3000 });
        },
        error: (err) => {
          console.error('Erreur lors de la mise à jour du statut du mouvement:', err);
          this._snackBar.open('Erreur lors de la mise à jour du statut du mouvement.', 'Erreur', { duration: 5000 });
        }
      });
  }

  // --- MISE À JOUR DE onReturn ---
  onReturn(movementTrace: any): void { // <-- Prend l'objet complet
    console.log(`[MovementTraceComponent] Initiation du retour pour le MovementTrace ID ${movementTrace?.id}`);

    // Vérifier que l'utilisateur est connecté
    if (!this.currentUser || !this.currentUser.id) {
      console.error('[MovementTraceComponent] Utilisateur non connecté ou ID manquant.');
      this._snackBar.open('Vous devez être connecté pour effectuer un retour.', 'Erreur', { duration: 5000 });
      return;
    }

    const currentUserId = this.currentUser.id; // Obtenir l'ID de l'utilisateur connecté

    // Appeler le service pour traiter le retour complet
    this._movementTraceService.processReturn(movementTrace, parseInt(currentUserId)) // <-- Passer l'objet complet et l'ID utilisateur
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: (results) => {
          console.log(`[MovementTraceComponent] Retour traité avec succès pour le MovementTrace ID ${movementTrace?.id}:`, results);
          // Afficher un message de succès
          this._snackBar.open(`Retour initié avec succès pour le mouvement ID ${movementTrace?.id}. Stock mis à jour.`, 'Succès', { duration: 5000 });
          
          // Optionnel : Rafraîchir la liste des MovementTraces si nécessaire
          this.loadMovementTraces();
        },
        error: (err) => {
          console.error(`[MovementTraceComponent] Erreur lors du traitement du retour pour le MovementTrace ID ${movementTrace?.id}:`, err);
          let errorMsg = 'Erreur lors de l\'initiation du retour.';
          if (err.status === 400) {
            errorMsg = 'Données de retour invalides.';
          } else if (err.status === 404) {
            errorMsg = 'Mouvement, Stock ou Article non trouvé.';
          } else if (err.status === 409) {
            errorMsg = 'Retour déjà initié pour ce mouvement.';
          } else if (err.status >= 500) {
            errorMsg = 'Erreur serveur.';
          } else if (err.message) {
            errorMsg = err.message;
          }
          this._snackBar.open(errorMsg, 'Erreur', { duration: 5000 });
        }
      });
  }
  // --- FIN DE LA MISE À JOUR ---
}
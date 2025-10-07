// src/app/modules/admin/apps/logistics/movement-traces/movement-trace.component.ts
import { MatDialogModule } from '@angular/material/dialog';
import { map, switchMap } from 'rxjs';
import { MovementTraceService } from './movment-trace.service'; // Vérifiez le nom du fichier
import { UserService } from 'app/core/user/user.service';
import { User } from 'app/core/user/user.types';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, Inject, OnDestroy, OnInit, Optional } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Subject, takeUntil, Unsubscribable } from 'rxjs';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDividerModule } from '@angular/material/divider';

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
                    <th mat-header-cell *matHeaderCellDef class="px-4 py-3 bg-gray-50 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Utilisateur</th>
                    <td mat-cell *matCellDef="let element" class="px-4 py-2 whitespace-nowrap text-sm text-gray-900">{{ element.userName }}</td>
                  </ng-container>

                  <!-- DetailPicklistId Column -->
                  <ng-container matColumnDef="detailPicklistId">
                    <th mat-header-cell *matHeaderCellDef class="px-4 py-3 bg-gray-50 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Détail Picklist Emplacement</th>
                    <td mat-cell *matCellDef="let element" class="px-4 py-2 whitespace-nowrap text-sm text-gray-900">{{ element.detailPicklistEmplacement }}</td>
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
                      <!-- <button mat-icon-button [color]="'accent'" (click)="onEdit(element)" title="Modifier">
                        <mat-icon>edit</mat-icon>
                      </button> -->
                      <!-- --- MISE À JOUR : Passer l'objet complet --- -->
                      <button mat-icon-button [color]="'warn'" (click)="onReturn(element)" title="Retourner">
                        <mat-icon>undo</mat-icon> <!-- Ou 'reply', 'keyboard_return', etc. -->
                      </button>
                      <!-- --- FIN DE LA MISE À JOUR --- -->
                      <!-- <button mat-icon-button [color]="element.isActive ? 'warn' : 'primary'" (click)="onToggleActive(element.id, element.isActive)" title="{{ element.isActive ? 'Désactiver' : 'Activer' }}">
                        <mat-icon>{{ element.isActive ? 'toggle_off' : 'toggle_on' }}</mat-icon>
                      </button> -->
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
    console.log('[MovementTraceComponent] Ouverture du dialogue de visualisation pour ID:', movementTrace.id);
    this._dialog.open(MovementTraceViewComponent, {
     minWidth: '600px',
    maxHeight: '90vh', // ✅ Limit height of modal
      disableClose: false,
      autoFocus: true,
      data: { movementTraceData: movementTrace } // Passer l'objet complet
    });
  }


 onEdit(movementTrace: any): void {
    console.log('[MovementTraceComponent] Ouverture du dialogue d\'édition pour ID:', movementTrace.id);
    const dialogRef = this._dialog.open(MovementTraceEditComponent, {
    minWidth: '600px',
    maxHeight: '90vh', // ✅ Limit height of modal      disableClose: false,
      autoFocus: true,
      data : { movementTraceData: movementTrace } // Passer l'objet complet
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('[MovementTraceComponent] Dialogue d\'édition fermé avec le résultat:', result);
      if (result === 'updated') {
        this._snackBar.open('Mouvement mis à jour avec succès.', 'Succès', { duration: 3000 });
        this.loadMovementTraces(); // Recharger la liste
      }
      // Gérer 'cancelled' ou d'autres résultats si nécessaire
    });
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
    console.log(`[MovementTracesComponent] Initiation du retour pour le MovementTrace ID ${movementTrace?.id}`);

    // Vérifier que l'utilisateur est connecté
    if (!this.currentUser || !this.currentUser.id) {
      console.error('[MovementTracesComponent] Utilisateur non connecté ou ID manquant.');
      this._snackBar.open('Vous devez être connecté pour effectuer un retour.', 'Erreur', { duration: 5000 });
      return;
    }

    const currentUserId = parseInt(this.currentUser.id, 10); // Obtenir l'ID de l'utilisateur connecté
    if (isNaN(currentUserId) || currentUserId <= 0) {
       console.error('[MovementTracesComponent] ID utilisateur invalide.');
       this._snackBar.open('ID utilisateur invalide.', 'Erreur', { duration: 5000 });
       return;
    }

    // Vérifier que le MovementTrace n'est pas déjà inactif
    if (!movementTrace.isActive) {
       console.warn(`[MovementTracesComponent] Le MovementTrace ID ${movementTrace.id} est déjà inactif.`);
       this._snackBar.open(`Le mouvement ID ${movementTrace.id} est déjà inactif.`, 'Avertissement', { duration: 3000 });
       return;
    }

    // --- MISE À JOUR : Enchaîner les appels API ---
    // 1. Appeler le service pour traiter le retour complet (ReturnLine + Sap)
    this._movementTraceService.processReturn(movementTrace, currentUserId).pipe(
      takeUntil(this._unsubscribeAll),
      // 2. Après le succès de processReturn, désactiver le MovementTrace
      switchMap((processReturnResults) => {
        console.log(`[MovementTracesComponent] processReturn réussi pour MovementTrace ID ${movementTrace.id}. Résultats:`, processReturnResults);
        // Retourner l'Observable pour setActiveStatus
        return this._movementTraceService.setActiveStatus(movementTrace.id, false).pipe(
          // Mapper le résultat pour inclure les résultats de processReturn
          map(setActiveResult => ({ processReturnResults, setActiveResult }))
        );
      })
    ).subscribe({
      next: (combinedResults) => {
        // combinedResults contient { processReturnResults: ..., setActiveResult: ... }
        console.log(`[MovementTracesComponent] Retour et désactivation traités avec succès pour le MovementTrace ID ${movementTrace.id}:`, combinedResults);
        // Afficher un message de succès
        this._snackBar.open(`Retour initié et mouvement ID ${movementTrace.id} désactivé avec succès. Stock mis à jour.`, 'Succès', { duration: 5000 });
        
        // Rafraîchir la liste des MovementTraces pour refléter le changement d'état
        this.loadMovementTraces();
      },
      error: (err) => {
        console.error(`[MovementTracesComponent] Erreur lors du traitement du retour ou de la désactivation pour le MovementTrace ID ${movementTrace.id}:`, err);
        let errorMsg = 'Erreur lors de l\'initiation du retour ou de la désactivation du mouvement.';
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
        // Ne pas fermer le dialogue pour permettre à l'utilisateur de réessayer
        // Gérer l'erreur (afficher un message, etc.)
      }
    });
    // --- FIN DE LA MISE À JOUR ---
  }

  // --- FIN DE LA MISE À JOUR ---
}










@Component({
  selector: 'app-movement-trace-view-dialog',
  template: `
    <div class="flex flex-col w-full h-full">
      <!-- Dialog Header -->
      <div class="flex items-center justify-between py-4 px-6 border-b">
        <div class="text-lg font-medium">Détails du Mouvement (ID: {{ data?.movementTraceData?.id }})</div>
        <button mat-icon-button (click)="onCancel()">
          <mat-icon [svgIcon]="'heroicons_outline:x-mark'"></mat-icon>
        </button>
      </div>

      <!-- Dialog Content -->
      <div class="flex-auto overflow-y-auto p-6 md:p-8">
        <mat-list *ngIf="data?.movementTraceData; else noData">
          <mat-list-item>
            <div class="flex justify-between w-full">
              <span class="font-medium">ID:</span>
              <span>{{ data.movementTraceData.id }}</span>
            </div>
          </mat-list-item>
          <mat-divider></mat-divider>
          <mat-list-item>
            <div class="flex justify-between w-full">
              <span class="font-medium">Code US:</span>
              <span>{{ data.movementTraceData.usNom }}</span>
            </div>
          </mat-list-item>
          <mat-divider></mat-divider>
          <mat-list-item>
            <div class="flex justify-between w-full">
              <span class="font-medium">Date du Mouvement:</span>
              <span>{{ data.movementTraceData.dateMouvement | date:'short' }}</span>
            </div>
          </mat-list-item>
          <mat-divider></mat-divider>
          <mat-list-item>
            <div class="flex justify-between w-full">
              <span class="font-medium">Quantité:</span>
              <span>{{ data.movementTraceData.quantite }}</span>
            </div>
          </mat-list-item>
          <mat-divider></mat-divider>
          <mat-list-item>
            <div class="flex justify-between w-full">
              <span class="font-medium">ID Utilisateur:</span>
              <span>{{ data.movementTraceData.userId }}</span>
            </div>
          </mat-list-item>
          <mat-divider></mat-divider>
          <mat-list-item>
            <div class="flex justify-between w-full">
              <span class="font-medium">ID Détail Picklist:</span>
              <span>{{ data.movementTraceData.detailPicklistId }}</span>
            </div>
          </mat-list-item>
          <mat-divider></mat-divider>
          <mat-list-item>
            <div class="flex justify-between w-full">
              <span class="font-medium">Statut:</span>
              <span [ngClass]="{
                'px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-green-100 text-green-800': data.movementTraceData.isActive,
                'px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-red-100 text-red-800': !data.movementTraceData.isActive
              }">
                {{ data.movementTraceData.isActive ? 'Actif' : 'Inactif' }}
              </span>
            </div>
          </mat-list-item>
          <!-- Ajoutez d'autres champs si nécessaire -->
        </mat-list>
        <ng-template #noData>
          <div class="text-center text-gray-500 p-4">Données de mouvement non disponibles.</div>
        </ng-template>
      </div>
    </div>
  `,
  styles: [`
    :host {
      display: flex;
      flex-direction: column;
      height: 100%;
    }
  `],
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
  ]
})
export class MovementTraceViewComponent {
  constructor(
    @Optional() @Inject(MAT_DIALOG_DATA) public data : { movementTraceData: any },
    public dialogRef: MatDialogRef<MovementTraceViewComponent>,
  ) {}

  onCancel(): void {
    this.dialogRef.close('cancelled');
  }
}
// --- Fin du composant MovementTraceViewComponent ---

// --- Movement Trace Edit Component (dans le même fichier) ---
@Component({
  selector: 'app-movement-trace-edit-dialog',
  template: `
    <div class="flex flex-col w-full h-full">
      <!-- Dialog Header -->
      <div class="flex items-center justify-between py-4 px-6 border-b">
        <div class="text-lg font-medium">Modifier le Mouvement (ID: {{ data?.movementTraceData?.id }})</div>
        <button mat-icon-button (click)="onCancel()" [disabled]="submitting">
          <mat-icon [svgIcon]="'heroicons_outline:x-mark'"></mat-icon>
        </button>
      </div>

      <!-- Dialog Content -->
      <div class="flex-auto overflow-y-auto p-6 md:p-8">
        <div class="flex flex-col items-center justify-center" *ngIf="loadingData">
          <mat-spinner diameter="40"></mat-spinner>
          <div class="mt-4 text-secondary">Chargement des détails...</div>
        </div>

        <form [formGroup]="movementTraceForm" (ngSubmit)="onSubmit()" class="flex flex-col" *ngIf="!loadingData">
          
          <mat-form-field class="w-full">
            <mat-label>Code US</mat-label>
            <input matInput formControlName="usNom" placeholder="Entrez le code US" required>
            <mat-error *ngIf="movementTraceForm.get('usNom')?.invalid && movementTraceForm.get('usNom')?.touched">
              <span *ngIf="movementTraceForm.get('usNom')?.errors?.['required']">Le code US est requis.</span>
            </mat-error>
          </mat-form-field>

          <mat-form-field class="w-full mt-4">
            <mat-label>Quantité</mat-label>
            <input matInput type="text" formControlName="quantite" placeholder="Entrez la quantité" required>
            <mat-error *ngIf="movementTraceForm.get('quantite')?.invalid && movementTraceForm.get('quantite')?.touched">
              <span *ngIf="movementTraceForm.get('quantite')?.errors?.['required']">La quantité est requise.</span>
            </mat-error>
          </mat-form-field>

          <mat-form-field class="w-full mt-4">
            <mat-label>ID Utilisateur</mat-label>
            <input matInput type="number" formControlName="userId" placeholder="Entrez l'ID de l'utilisateur" required min="1">
            <mat-error *ngIf="movementTraceForm.get('userId')?.invalid && movementTraceForm.get('userId')?.touched">
              <span *ngIf="movementTraceForm.get('userId')?.errors?.['required']">L'ID de l'utilisateur est requis.</span>
              <span *ngIf="movementTraceForm.get('userId')?.errors?.['min']">L'ID doit être positif.</span>
            </mat-error>
          </mat-form-field>

          <mat-form-field class="w-full mt-4">
            <mat-label>ID Détail Picklist</mat-label>
            <input matInput type="number" formControlName="detailPicklistId" placeholder="Entrez l'ID du détail picklist" required min="1">
            <mat-error *ngIf="movementTraceForm.get('detailPicklistId')?.invalid && movementTraceForm.get('detailPicklistId')?.touched">
              <span *ngIf="movementTraceForm.get('detailPicklistId')?.errors?.['required']">L'ID du détail picklist est requis.</span>
              <span *ngIf="movementTraceForm.get('detailPicklistId')?.errors?.['min']">L'ID doit être positif.</span>
            </mat-error>
          </mat-form-field>

          <mat-form-field class="w-full mt-4">
            <mat-label>Statut</mat-label>
            <mat-select formControlName="isActive" placeholder="Sélectionnez un statut" required>
              <mat-option [value]="true">Actif</mat-option>
              <mat-option [value]="false">Inactif</mat-option>
            </mat-select>
            <mat-error *ngIf="movementTraceForm.get('isActive')?.invalid && movementTraceForm.get('isActive')?.touched">
              Le statut est requis.
            </mat-error>
          </mat-form-field>

          <div class="flex items-center justify-end mt-6">
            <button
              mat-stroked-button
              type="button"
              (click)="onCancel()"
              [disabled]="submitting"
            >
              Annuler
            </button>
            <button
              mat-flat-button
              type="submit"
              [color]="'primary'"
              class="ml-3"
              [disabled]="movementTraceForm.invalid || submitting"
            >
              <mat-progress-spinner
                *ngIf="submitting"
                [diameter]="20"
                [mode]="'indeterminate'"
                class="mr-2"
              >
              </mat-progress-spinner>
              <span *ngIf="!submitting">Mettre à jour</span>
              <span *ngIf="submitting">Mise à jour...</span>
            </button>
          </div>
        </form>
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
  `],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    MatDividerModule,
  ]
})
export class MovementTraceEditComponent implements OnInit {
  movementTraceForm: FormGroup;
  submitting: boolean = false;
  loadingData: boolean = false;

  constructor(
    private _formBuilder: FormBuilder,
    private _snackBar: MatSnackBar,
    private _movementTraceService: MovementTraceService,
    public dialogRef: MatDialogRef<MovementTraceEditComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data :  { movementTraceData: any }
  ) {
    this.movementTraceForm = this._formBuilder.group({
      usNom: ['', [Validators.required]],
      quantite: ['', [Validators.required]],
      userId: [null, [Validators.required, Validators.min(1)]],
      detailPicklistId: [null, [Validators.required, Validators.min(1)]],
      isActive: [true, [Validators.required]]
    });
  }

  ngOnInit(): void {
    if (this.data?.movementTraceData) {
      console.log('[MovementTraceEditComponent] Données de mouvement reçues pour édition:', this.data.movementTraceData);
      this.movementTraceForm.patchValue({
        ...this.data.movementTraceData,
        // S'assurer que les booléens/numériques sont correctement typés si nécessaire
        isActive: this.data.movementTraceData.isActive === true // Comparaison stricte
      });
      this.loadingData = false;
    } else {
      console.error('[MovementTraceEditComponent] Aucune donnée de mouvement fournie au dialogue d\'édition.');
      this.loadingData = false;
      this.dialogRef.close();
    }
  }

  onSubmit(): void {
    if (this.movementTraceForm.invalid) {
      this.movementTraceForm.markAllAsTouched();
      return;
    }

    this.submitting = true;
    const movementTraceId = this.data?.movementTraceData?.id;
    const formData = this.movementTraceForm.value;

    // if (movementTraceId) {
    //   console.log(`[MovementTraceEditComponent] Appel du service pour mettre à jour le mouvement ID ${movementTraceId} avec:`, formData);
    //   this._movementTraceService.update(movementTraceId, formData)
    //     .pipe(takeUntil(new Subject())) // Utiliser un Subject local ou this.destroy$ si disponible
    //     .subscribe({
    //       next: (response) => {
    //         console.log(`[MovementTraceEditComponent] Mouvement ID ${movementTraceId} mis à jour avec succès.`, response);
    //         this.submitting = false;
    //         this.dialogRef.close('updated');
    //       },
    //       error: (err) => {
    //         console.error(`[MovementTraceEditComponent] Erreur lors de la mise à jour du mouvement ID ${movementTraceId}:`, err);
    //         this.submitting = false;
    //         let errorMsg = 'Erreur lors de la mise à jour du mouvement.';
    //         if (err.status === 400) {
    //           errorMsg = 'Données de mouvement invalides.';
    //         } else if (err.status === 404) {
    //           errorMsg = 'Mouvement non trouvé.';
    //         } else if (err.status >= 500) {
    //           errorMsg = 'Erreur serveur.';
    //         }
    //         this._snackBar.open(errorMsg, 'Erreur', { duration: 5000 });
    //       }
    //     });
    // } else {
    //   this.submitting = false;
    //   console.error('[MovementTraceEditComponent] Impossible de mettre à jour : ID de mouvement manquant.');
    //   this._snackBar.open('Impossible de mettre à jour : ID de mouvement manquant.', 'Erreur', { duration: 5000 });
    // }
  }

  onCancel(): void {
    this.dialogRef.close('cancelled');
  }
}
// --- Fin du composant MovementTraceEditComponent ---
import { AfterViewInit, Component, ElementRef, Inject, OnInit, QueryList, ViewChildren } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { PicklistService } from '../picklist.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import JsBarcode from 'jsbarcode';

@Component({
  selector: 'app-details',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  // Property to hold the calculated total quantity
  totalPicklistQuantity: number = 0;
  isAllAvailable: boolean = false; // Initialize

  @ViewChildren('barcodeRef') barcodeElements!: QueryList<ElementRef>;
  picklistId!: number;
  picklist: any;
  detailPicklists: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private picklistService: PicklistService,
    private _snackBar: MatSnackBar,
    private dialog: MatDialog
  ) { }

sendOutPicklist() {
  if (this.isAllAvailable && this.picklist) { 
    const updateData = {
      name: this.picklist.name,
      type: this.picklist.type,

      quantity: this.totalPicklistQuantity?.toString() ?? this.picklist.quantity, 
      lineId: this.picklist.line?.id ?? this.picklist.lineId, // Use nested object or direct ID
      warehouseId: this.picklist.warehouse?.id ?? this.picklist.warehouseId, // Use nested object or direct ID
     
      statusId: 2 
    };

    console.log(`Updating picklist ${this.picklistId} with data:`, updateData); // Debug log

    // 2. Call the update service method
    this.picklistService.updatePicklist(this.picklistId, updateData).subscribe({
      next: (response) => { // Handle successful response
        console.log('Picklist updated successfully:', response);
        this._snackBar.open('Picklist envoyée avec succès', 'Succès', { duration: 3000 });
        
        // 3. Refresh picklist data to reflect the change
        this.loadPicklist(); 
        // If details also show status, reload them too
        // this.loadDetailPicklists(this.picklistId); 
      },
      error: (e) => { // Handle errors
        console.error('Error sending picklist:', e);
        // Try to get a more specific error message from the backend response
        let errorMsg = 'Erreur lors de l\'envoi de la picklist';
        if (e.error && e.error.message) {
          errorMsg = e.error.message;
        } else if (e.message) {
          errorMsg = e.message;
        }
        this._snackBar.open(errorMsg, 'Erreur', { duration: 5000 }); // Longer duration for errors
      }
    });
  } else {
    // Provide feedback if conditions aren't met
    if (!this.picklist) {
        this._snackBar.open('Données de picklist non chargées', 'Erreur', { duration: 5000 });
    } else if (!this.isAllAvailable) {
        this._snackBar.open('Certaines quantités ne sont pas disponibles', 'Erreur', { duration: 5000 });
    }
  }
}



checkAvailabilityAndShowDialog() {
  if (this.detailPicklists.length === 0) {
    this._snackBar.open('Aucun détail à vérifier.', 'Info', { duration: 3000 });
    return;
  }

  // Prepare data for the backend check (same as before)
  const detailsForCheck = this.detailPicklists.map(detail => ({ ...detail }));

  this.picklistService.checkInventoryAvailability(detailsForCheck).subscribe({
    next: (availability) => {
      if (availability.length !== this.detailPicklists.length) {
        console.error('Availability response length mismatch');
        this._snackBar.open('Erreur de données de disponibilité', 'Erreur', { duration: 5000 });
        return;
      }

      // Update detailPicklists with availability information (same as before)
      this.detailPicklists = this.detailPicklists.map((detail, index) => ({
        ...detail,
        isAvailable: availability[index].isAvailable,
        availableQuantity: availability[index].availableQuantity
      }));

      // Determine if all items are available (same as before)
      this.isAllAvailable = this.detailPicklists.every(detail => detail.isAvailable === true);

      this._snackBar.open('Disponibilité vérifiée.', 'Succès', { duration: 3000 });

      // --- Open the dialog AFTER the data is updated ---
      const dialogRef = this.dialog.open(AvailabilityDialogComponent, {
        width: '600px',
        data: { details: this.detailPicklists } // Now passes updated data
      });

      dialogRef.afterClosed().subscribe(result => {
        // Handle dialog close if needed (e.g., refresh parent component)
        // Note: Calling checkAvailability again here might be redundant
        // unless the dialog itself can modify inventory/trigger a recheck.
        // if (result) {
        //   this.checkAvailability(); // Remove this to avoid loop
        // }
      });
    },
    error: (e) => {
      console.error('Error checking availability:', e);
      let errorMsg = 'Erreur lors de la vérification de la disponibilité';
      if (e.error && e.error.message) {
        errorMsg = e.error.message;
      }
      this._snackBar.open(errorMsg, 'Erreur', { duration: 5000 });
    
    }
  });
}



checkAvailability() {
  if (this.detailPicklists.length === 0) {
    this._snackBar.open('Aucun détail à vérifier.', 'Info', { duration: 3000 });
    return;
  }

  // Prepare data for the backend check
  // Send the detail picklist items. The backend should be able to identify them
  // via their 'id' field to correlate with the response.
  const detailsForCheck = this.detailPicklists.map(detail => ({
    // Send the whole detail object or just the ID if the backend only needs that for correlation.
    // Sending the whole object is safer if the backend needs other fields for the check.
    ...detail
    // Alternatively, if backend only needs ID:
    // id: detail.id
  }));
  console.log('Sending for availability check:', detailsForCheck);

  this.picklistService.checkInventoryAvailability(detailsForCheck).subscribe({
    next: (availability) => { // Type this properly if you have the DTO interface
      console.log('Availability response received:', availability);
      if (availability.length !== this.detailPicklists.length) {
        console.error('Availability response length mismatch');
        this._snackBar.open('Erreur de données de disponibilité', 'Erreur', { duration: 5000 });
        return;
      }

      // --- KEY CHANGE HERE ---
      // Update detailPicklists by *merging* availability info, not replacing objects
      // Use map to create a new array, ensuring change detection works
      const updatedDetails = this.detailPicklists.map((detail, index) => ({
        // Spread the original detail object to keep all its properties
        ...detail,
        // Add or overwrite the availability properties from the response
        isAvailable: availability[index].isAvailable,
        availableQuantity: availability[index].availableQuantity
        // requestedQuantity and codeProduit are in the response but might not be needed
        // on the main object if they are already present (e.g., detail.quantite, detail.article?.codeProduit)
        // requestedQuantity: availability[index].requestedQuantity,
        // codeProduit: availability[index].codeProduit
      }));

      // 1. Update the component's property with the merged data
      this.detailPicklists = updatedDetails;
      console.log(this.detailPicklists);
      

      // 2. Determine if all items are available
      this.isAllAvailable = this.detailPicklists.every(detail => detail.isAvailable === true);
      console.log('All items available:', this.isAllAvailable);

      // 3. Show success snackbar
      this._snackBar.open('Disponibilité vérifiée.', 'Succès', { duration: 3000 });

      // 4. --- Open the dialog AFTER the data is updated ---
      const dialogRef = this.dialog.open(AvailabilityDialogComponent, {
        width: '600px',
        // Pass the updated data
        data: { details: this.detailPicklists } // or [...this.detailPicklists]
      });

      dialogRef.afterClosed().subscribe(result => {
        console.log('Dialog closed with result:', result);
        this.generateBarcodes();
        // Handle dialog close if needed in the future
      });
    },
    error: (e) => {
      console.error('Error checking availability:', e);
      let errorMsg = 'Erreur lors de la vérification de la disponibilité';
      if (e.error && e.error.message) {
        errorMsg = e.error.message;
      }
      this._snackBar.open(errorMsg, 'Erreur', { duration: 5000 });
    }
  });
}

  generateBarcodes() {
    // Use setTimeout to ensure the view is fully rendered before accessing elements
    setTimeout(() => {
      if (this.barcodeElements && this.barcodeElements.length > 0) {
        this.barcodeElements.forEach((el: ElementRef, index: number) => {
          const code = this.detailPicklists[index]?.article?.codeProduit || '000000';
          // console.log(`Generating barcode for detail ${index}: ${code}`); // Optional debug log

          JsBarcode(el.nativeElement, code, {
            format: 'CODE128',
            lineColor: '#FFFFFF',
            background: '#374151', // Tailwind's gray-700
            width: 2,
            height: 24,
            displayValue: true,
            // Add error handling for barcode generation
            // Note: JsBarcode doesn't have a direct error callback in this syntax,
            // but invalid codes usually result in a default barcode or placeholder.
          });
        });
      }
    }, 0); // Very short delay, mainly to push to the end of the execution queue
  }

  calculateTotalQuantity() {
    this.totalPicklistQuantity = this.detailPicklists.reduce((sum, detail) => {
      const quantite = parseInt(detail.quantite, 10);
      return sum + (isNaN(quantite) ? 0 : quantite);
    }, 0);
    console.log('Total Picklist Quantity Calculated:', this.totalPicklistQuantity);
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.picklistId = +params['id'];
      if (this.picklistId) { // Basic check
         this.loadPicklist();
         this.loadDetailPicklists(this.picklistId);
      } else {
         this._snackBar.open('ID de picklist invalide', 'Erreur', { duration: 5000 });
      }
    });
  }

  loadPicklist() {
    this.picklistService.getPicklistById(this.picklistId).subscribe({
      next: (p) => {
        this.picklist = p;
        console.log('Picklist loaded:', this.picklist);
      },
      error: (e) => {
        console.error('Error loading picklist:', e);
        this._snackBar.open('Erreur chargement picklist', 'Erreur', { duration: 5000 });
      }
    });
  }

  loadDetailPicklists(id: number) {
    this.picklistService.loadDetailPicklists(id).subscribe({
      next: (d) => {
        this.detailPicklists = d;
        console.log('Detail Picklists loaded:', this.detailPicklists);

        // Calculate total quantity after loading details
        this.calculateTotalQuantity();

        // Check availability after loading details (optional, can be triggered by button click)
        // this.checkAvailability();

        // Generate barcodes after loading details
        this.generateBarcodes(); // This now uses setTimeout internally
      },
      error: e => {
        console.error('Error loading detail picklists:', e);
        // Provide more specific error message if possible from e.error.message
        let errorMsg = 'Erreur chargement détails';
        if (e.error && e.error.message) {
            errorMsg = e.error.message;
        }
        this._snackBar.open(errorMsg, 'Erreur', { duration: 5000 });
      }
    });
  }
}


@Component({
  selector: 'app-availability-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatTableModule, MatButtonModule], // Added MatButtonModule
  template: `
    <h1 mat-dialog-title>Vérification de la Disponibilité</h1>
    <div mat-dialog-content>
      <table mat-table [dataSource]="data.details" class="min-w-full">
        <!-- Article Column -->
        <ng-container matColumnDef="article">
          <th mat-header-cell *matHeaderCellDef class="px-4 py-2 text-left">Article</th>
          <td mat-cell *matCellDef="let detail" class="px-4 py-2 border-b">{{ detail.article?.designation }}</td>
        </ng-container>

        <!-- Required Quantity Column -->
        <ng-container matColumnDef="quantite">
          <th mat-header-cell *matHeaderCellDef class="px-4 py-2 text-left">Quantité Requise</th>
          <td mat-cell *matCellDef="let detail" class="px-4 py-2 border-b">{{ detail.quantite }}</td>
        </ng-container>

        <!-- Availability Column -->
        <ng-container matColumnDef="available">
          <th mat-header-cell *matHeaderCellDef class="px-4 py-2 text-left">Disponibilité</th>
          <td mat-cell *matCellDef="let detail" class="px-4 py-2 border-b">
            <span [ngClass]="{
              'text-green-400 font-semibold': detail.isAvailable === true,
              'text-red-400 font-semibold': detail.isAvailable === false,
              'text-yellow-400': detail.isAvailable !== true && detail.isAvailable !== false
            }">
              <span *ngIf="detail.isAvailable === true">Disponible</span>
              <span *ngIf="detail.isAvailable === false">Non disponible</span>
              <span *ngIf="detail.isAvailable !== true && detail.isAvailable !== false">Vérification...</span>
              <span *ngIf="detail.availableQuantity !== undefined"> ({{ detail.availableQuantity }})</span>
            </span>
          </td>
        </ng-container>

        <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
        <tr mat-row *matRowDef="let row; columns: displayedColumns;"
            [ngClass]="{
              'bg-green-900/30': row.isAvailable === true,
              'bg-red-900/30': row.isAvailable === false
            }"></tr>
      </table>
    </div>
    <div mat-dialog-actions class="flex justify-end p-4">
      <button mat-button (click)="onClose()">Fermer</button>
    </div>
  `,
  styles: [`
    /* Optional: Add basic table styling if not covered by global styles */
    table { width: 100%; border-collapse: collapse; }
    th, td { padding: 8px; text-align: left; }
    th { background-color: #1f2937; /* gray-800 */ }
    tr:hover { background-color: #374151; /* gray-700 */ }
    .text-green-400 { color: #4ade80; } /* Tailwind green-400 */
    .text-red-400 { color: #f87171; }   /* Tailwind red-400 */
    .text-yellow-400 { color: #fbbf24; } /* Tailwind yellow-400 */
    .font-semibold { font-weight: 600; }
    .bg-green-900\\/30 { background-color: rgba(16, 185, 129, 0.1); } /* green-500 with opacity */
    .bg-red-900\\/30 { background-color: rgba(239, 68, 68, 0.1); }   /* red-500 with opacity */
  `]
})
export class AvailabilityDialogComponent {
  displayedColumns: string[] = ['article', 'quantite', 'available'];

  constructor(
    public dialogRef: MatDialogRef<AvailabilityDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { details: any[] }
  ) {
    // Optional: Log received data for debugging
    // console.log('Dialog received data:', data);
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
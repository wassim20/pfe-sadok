import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseCompanyService } from 'app/core/services/base-company.service';
import { AuthService } from 'app/core/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService extends BaseCompanyService {

  protected apiUrl = 'http://localhost:5288/api/Warehouses';

  constructor(http: HttpClient, authService: AuthService) {
    super(http, authService);
  }

  // 🏢 Company-aware methods (inherited from BaseCompanyService)
  // - getAllByCompany(isActive?: boolean): Observable<any[]>
  // - getByIdAndCompany(id: number): Observable<any>
  // - createForCompany(dto: any): Observable<any>
  // - updateForCompany(id: number, dto: any): Observable<any>
  // - setActiveStatusForCompany(id: number, value: boolean): Observable<any>

  // Legacy methods for backward compatibility
  getWarehouses(): Observable<any[]> {
    return this.getAllByCompany(); // 🏢 Use company-aware method
  }

  createWarehouse(dto: { name: string; description: string }): Observable<any> {
    return this.createForCompany(dto); // 🏢 Use company-aware method
  }

  updateWarehouse(id: number, dto: { name: string; description: string }): Observable<any> {
    return this.updateForCompany(id, dto); // 🏢 Use company-aware method
  }

  setActiveStatus(id: number, value: boolean): Observable<any> {
    return this.setActiveStatusForCompany(id, value); // 🏢 Use company-aware method
  }
}




// src/app/core/services/location.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseCompanyService } from 'app/core/services/base-company.service';
import { AuthService } from 'app/core/auth/auth.service';


@Injectable({
  providedIn: 'root'
})
export class LocationService extends BaseCompanyService {
  protected apiUrl = 'http://localhost:5288/api/Locations'; // Base URL de votre API

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
  // GET: /api/Locations?isActive=true
  getLocations(isActive: boolean | null = true): Observable<any[]> {
    return this.getAllByCompany(isActive); // 🏢 Use company-aware method
  }

  // GET: /api/Locations/{id}
  getLocationById(id: number): Observable<any> {
    return this.getByIdAndCompany(id); // 🏢 Use company-aware method
  }

  // POST: /api/Locations
  createLocation(location: any): Observable<any> {
    return this.createForCompany(location); // 🏢 Use company-aware method
  }

  // PUT: /api/Locations/{id}
  updateLocation(id: number, location: any): Observable<any> {
    return this.updateForCompany(id, location); // 🏢 Use company-aware method
  }

  // PUT: /api/Locations/{id}/set-active?value=true/false
  setActiveStatus(id: number, value: boolean): Observable<any> {
    return this.setActiveStatusForCompany(id, value); // 🏢 Use company-aware method
  }
  // location.service.ts



  getWarehouses(isActive: boolean | null = true) {
    // Use the correct Warehouses API endpoint - FIXED
    let params = new HttpParams();
    if (isActive !== null) {
      params = params.append('isActive', isActive.toString());
    }
    return this.http.get<any[]>(`http://localhost:5288/api/Warehouses`, { params });
  }

}
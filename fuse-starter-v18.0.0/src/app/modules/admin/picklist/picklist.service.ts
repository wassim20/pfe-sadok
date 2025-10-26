import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseCompanyService } from 'app/core/services/base-company.service';
import { AuthService } from 'app/core/auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class PicklistService extends BaseCompanyService {

  protected apiUrl = 'http://localhost:5288/api/Picklists';

  constructor(http: HttpClient, authService: AuthService) {
    super(http, authService);
  }

  // ✅ Get all picklists - Company-aware
  getPicklists(isActive: boolean = true): Observable<any[]> {
    return this.getAllByCompany(isActive);
  }

  // ✅ Get picklist by ID - Company-aware
  getPicklistById(id: number): Observable<any> {
    return this.getByIdAndCompany(id);
  }

  // ✅ Create new picklist - Company-aware
  createPicklist(data: any): Observable<any> {
    return this.createForCompany(data);
  }

  // ✅ Update existing picklist - Company-aware
  updatePicklist(id: number, data: any): Observable<any> {
    return this.updateForCompany(id, data);
  }

  // ✅ Set active/inactive status - Company-aware
  setActiveStatus(id: number, value: boolean): Observable<any> {
    return this.setActiveStatusForCompany(id, value);
  }

  // ✅ Transitions
  markReady(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/ready`, {});
  }
  startShipping(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/ship`, {});
  }
  complete(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/complete`, {});
  }
  

   getWarehouses(): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:5288/api/Warehouses`);
  }
   getlines(): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:5288/api/Lines`);
  }
   getstatus(): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:5288/api/Status`);
  }
   // ✅ Load picklist details - Company-aware
  loadDetailPicklists(id: number): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:5288/api/DetailPicklists/by-picklist/${id}`);
  }

  // ✅ Load picklist details with availability status - Enhanced
  loadDetailPicklistsWithAvailability(id: number): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:5288/api/DetailPicklists/by-picklist/${id}/with-availability`);
  }

  // ✅ Check inventory availability - Enhanced
  checkInventoryAvailability(details: any[]): Observable<any[]> {
    return this.http.post<any[]>(`http://localhost:5288/api/DetailPicklists/check-availability`, details);
  }

  // ✅ Create detail picklist - Company-aware
  createDetailPicklist(createDto: any): Observable<any> {
    const url = `http://localhost:5288/api/DetailPicklists`;
    console.log(`[PicklistService] Creating detail picklist:`, createDto);
    return this.http.post<any>(url, createDto);
  }

  // ✅ Get articles - Company-aware
  getArticles(): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:5288/api/Articles?isActive=true');
  }

  // ✅ Create movement trace - Company-aware
  createMovementTrace(dto: any): Observable<any> {
    return this.http.post<any>(`http://localhost:5288/api/MovementTraces`, dto);
  }

  // ✅ Delete detail picklist - Company-aware
  deleteDetailPicklist(id: number): Observable<any> {
    return this.http.delete<any>(`http://localhost:5288/api/DetailPicklists/${id}`);
  }
}

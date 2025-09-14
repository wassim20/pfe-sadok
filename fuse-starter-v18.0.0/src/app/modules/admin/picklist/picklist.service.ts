import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PicklistService {
  
  private apiUrl = 'http://localhost:5288/api/Picklists';

  constructor(private http: HttpClient) {}

  // ✅ Get all picklists, optionally filtered by active status
  getPicklists(isActive: boolean = true): Observable<any[]> {
    const params = new HttpParams().set('isActive', isActive.toString());
    return this.http.get<any[]>(this.apiUrl, { params });
  }

  // ✅ Get picklist by ID
  getPicklistById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  // ✅ Create new picklist
  createPicklist(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  // ✅ Update existing picklist
  updatePicklist(id: number, data: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }

  // ✅ Set active/inactive status
  setActiveStatus(id: number, value: boolean): Observable<any> {
    const params = new HttpParams().set('value', value.toString());
    return this.http.put(`${this.apiUrl}/${id}/set-active`, null, { params });
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
   loadDetailPicklists(id:number): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:5288/api/detailpicklists/by-picklist/${id}`);
  }
  

    checkInventoryAvailability(details: any[]): Observable<{ detailPicklistId: number, isAvailable: boolean, availableQuantity: number, requestedQuantity: number, codeProduit: string }[]> {
    return this.http.post<any[]>(`http://localhost:5288/api/detailpicklists/check-availability`, details);
  }

  updatePicklistStatus(id:number,data: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, { data });
  }



  
}

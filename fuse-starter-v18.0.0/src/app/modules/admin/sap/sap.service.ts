import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SapService {
 
  private apiUrl = 'http://localhost:5288/api/Sap';

  constructor(private http: HttpClient) {}

  // Read all SAP entries
 getAll(): Observable<any[]> {
    // Convert the Promise-based approach to an Observable
    return from(
      Promise.all([
        this.http.get<any[]>(`${this.apiUrl}?isActive=true`).toPromise(),
        this.http.get<any[]>(`${this.apiUrl}?isActive=false`).toPromise()
      ]).then(([activeSaps, inactiveSaps]) => {
        // Combine active and inactive SAP entries into a single array
        return [...(activeSaps || []), ...(inactiveSaps || [])];
      }).catch(error => {
        throw error; // Propagate error to the Observable
      })
    );
  }

  // Create a new SAP entry
  create(sap: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, sap);
  }

  // Update an existing SAP entry
  update(id: number, sap: any): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, sap);
  }

  // Delete an SAP entry
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  setactive(id: number, active: boolean): Observable<any> { // Ou setActiveStatus
  const params = new HttpParams().set('value', active.toString());
  return this.http.put<void>(`${this.apiUrl}/${id}/set-active`, {}, { params });
}
   
}
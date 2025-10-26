// src/app/core/services/location.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseCompanyService } from 'app/core/services/base-company.service';
import { AuthService } from 'app/core/auth/auth.service';



// Interfaces pour ReturnLine (basées sur le Swagger/DTOs)
export interface ReturnLineReadDto {
  id: number;
  dateRetour: string; // Format ISO 8601
  quantite: string;
  usCode: string;
  articleId: number;
  articleCode :string;
  userId: number;
  userName :string;
  statusId: number;
}

export interface ReturnLineCreateDto {
  dateRetour: string; // Format ISO 8601 ou Date
  quantite: string;
  usCode: string;
  articleId: number;
  userId: number; // ID de l'utilisateur effectuant le retour
  statusId: number; // ID du statut initial (ex: EnAttente)
}

export interface ReturnLineUpdateDto {
  // Généralement, on met à jour peu de champs directement.
  // La logique métier gère souvent les transitions de statut.
  // Pour cet exemple, permettons la mise à jour de quelques champs.
  dateRetour?: string; // Optionnel pour la mise à jour
  quantite?: string;   // Optionnel
  usCode?: string;    // Optionnel
  articleId?: number;  // Optionnel
  userId?: number;    // Optionnel
  statusId?: number;  // Permet de changer le statut
}

@Injectable({
  providedIn: 'root'
})
export class ReturnService extends BaseCompanyService {

  protected apiUrl = 'http://localhost:5288/api/ReturnLines'; // Ajustez le port si nécessaire

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
  getAll(): Observable<ReturnLineReadDto[]> {
    return this.getAllByCompany(); // 🏢 Use company-aware method
  }

  getById(id: number): Observable<ReturnLineReadDto> {
    return this.getByIdAndCompany(id); // 🏢 Use company-aware method
  }

  create(dto: ReturnLineCreateDto): Observable<ReturnLineReadDto> {
    return this.createForCompany(dto); // 🏢 Use company-aware method
  }

  update(id: number, dto: ReturnLineUpdateDto): Observable<any> { // PUT souvent retourne 204 No Content
    return this.updateForCompany(id, dto); // 🏢 Use company-aware method
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  // Exemple : Mettre à jour le statut (alternatif à update)
  // setStatus(id: number, statusId: number): Observable<any> {
  //   const params = new HttpParams().set('statusId', statusId.toString());
  //   return this.http.put(`${this.apiUrl}/${id}/status`, {}, { params });
  // }


}

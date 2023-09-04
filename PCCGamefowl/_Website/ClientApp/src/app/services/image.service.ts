import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';


@Injectable({ providedIn: 'root' })
export class ImageService {
  private _baseUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  getImage(id: string): Observable<any> {
    return this.http.get(this._baseUrl + 'api/uploadFile/getImage/' + id);
  }
}

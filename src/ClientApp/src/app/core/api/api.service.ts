import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Subscriber } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ApiService {

    constructor(private http: HttpClient) { }
}

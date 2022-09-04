import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    private url = `${environment.apiBaseUrl}/Account`;

    constructor(private httpClient: HttpClient) { }

    getAll() {
        return this.httpClient.get(this.url);
    }

    getById(id: number) {
        var uri = `${this.url}/${id}`;
        return this.httpClient.get(uri);
    }

    post(data: any) {
        return this.httpClient.post(this.url, data);
    }

    patch(data: any) {
        return this.httpClient.patch(this.url, data);
    }

    delete(id: number) {
        var uri = `${this.url}/${id}`;
        return this.httpClient.delete(uri);
    }
}
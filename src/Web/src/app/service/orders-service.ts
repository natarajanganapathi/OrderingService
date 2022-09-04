import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class OrdersService {
    private url = `${environment.apiBaseUrl}/Orders`;

    constructor(private httpClient: HttpClient) { }

    getAll() {
        return this.httpClient.get(this.url);
    }

    getById(id: number) {
        var uri = `${this.url}/${id}`;
        return this.httpClient.get(uri);
    }

    create(data: any) {
        return this.httpClient.post(this.url, data);
    }

    update(id: number, data: any) {
        var uri = `${this.url}/${id}`;
        return this.httpClient.post(uri, data);
    }

    delete(id: number) {
        var uri = `${this.url}/${id}`;
        return this.httpClient.delete(uri);
    }

}
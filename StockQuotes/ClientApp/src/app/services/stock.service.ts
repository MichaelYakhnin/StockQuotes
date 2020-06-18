import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Stock } from '../models/stock';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  baseUrl: string;
  api = 'stocksquotes/';
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
   }

  getQuotes(symbol: string): any {
     return this.http.get(this.baseUrl + this.api + 'quotes?symbol=' + symbol);
   // return this.http.get('/assets/quotes.json');
  }
  getStockList(): Observable<Stock[]> {
    return this.http.get<Stock[]>(this.baseUrl +  this.api + 'stocks');
    // return this.http.get('/assets/stocks.json');
  }

}

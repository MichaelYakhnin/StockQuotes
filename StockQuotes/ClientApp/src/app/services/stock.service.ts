import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  baseUrl: string;
  api = 'stocksquotes/';
  stocks: any;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getQuotes(symbol: string): any {
    return this.http.get(this.baseUrl + this.api + 'quotes?symbol=' + symbol);
    // return this.http.get('/assets/quotes.json');
  }
  getStockList(): any {
    if (!this.stocks) {
      this.stocks = this.http.get(this.baseUrl + this.api + 'stocks');
    }

    return this.stocks;
    // return this.http.get('/assets/stocks.json');
  }

}

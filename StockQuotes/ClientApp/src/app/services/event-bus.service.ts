import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EventBusService {
  public symbolChanged = new EventEmitter<string>();

  onSymbolChanged(symbol: string) {
    this.symbolChanged.emit(symbol);
  }

}

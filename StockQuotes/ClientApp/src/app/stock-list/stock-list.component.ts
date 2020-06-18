import { Component, OnInit} from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { StockService } from '../services/stock.service';
import { EventBusService } from '../services/event-bus.service';

@Component({
  selector: 'app-stock-list',
  templateUrl: './stock-list.component.html',
  styleUrls: ['./stock-list.component.css']
})
export class StockListComponent implements OnInit {
  rows = [];
  columns: any[];
  
  constructor(private stockService: StockService,
    private eventBusService: EventBusService,
     private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.columns = [
      { prop: 'symbol', name: 'Symbol'},
      { prop: 'name', name: 'Name'},
      { prop: 'price' , name: 'Price'},
      { prop: 'exchange' , name: 'Exchange'}
    ];
    this.spinner.show();
    this.stockService.getStockList().subscribe(list => {
      this.rows = list;
      this.spinner.hide();
    });
  }
  onActive(event) {
    if (event.type === 'click') {
      this.eventBusService.onSymbolChanged(event.row.symbol);
    }
  }

}

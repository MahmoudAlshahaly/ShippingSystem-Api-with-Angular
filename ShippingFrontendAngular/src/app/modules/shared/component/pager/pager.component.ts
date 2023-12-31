import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.css']
})
export class PagerComponent  implements OnInit{

  @Input() pageSize?: number; 
  @Input() totalCount?: number; 
  @Output() pageChanged = new EventEmitter<number>();
  constructor() {
  
    
  }
  ngOnInit(): void {
   
  }
  onPagerChanged(event: any)
  {
    this.pageChanged.emit(event);
  }
}

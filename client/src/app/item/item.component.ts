import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css'],
})
export class ItemComponent {
  editable = false;

  @Input() item!: string;
  @Output() remove = new EventEmitter<string>();

  constructor() {
    console.log('item', this.item);
  }
}

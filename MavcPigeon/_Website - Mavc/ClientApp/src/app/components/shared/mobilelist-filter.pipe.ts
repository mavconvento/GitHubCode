import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
  name: 'mobileFilter'
})
export class MobileListFilterPipe implements PipeTransform {
  transform(items: any[], searchVal: any): any {
    let filteredItems = [];
    if (items && searchVal) {
      filteredItems = items.filter(i => i.ClubID == searchVal);
    }

    
    return filteredItems;
  }
}

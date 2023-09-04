import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
  name: 'clubFilter'
})
export class ClubListFilterPipe implements PipeTransform {
  transform(items: any[], searchVal: any): any {

    let filteredItems = [];
    if (items && searchVal) {
      filteredItems = items.filter(x => x.ClubName.toUpperCase().indexOf(searchVal.toUpperCase()));
    }

    console.log(searchVal);
    return filteredItems;
  }
}

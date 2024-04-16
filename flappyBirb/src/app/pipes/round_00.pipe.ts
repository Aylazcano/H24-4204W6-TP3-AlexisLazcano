import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'round_00'
})
export class Round_00Pipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return Math.round(value * 100) / 100;
  }

}

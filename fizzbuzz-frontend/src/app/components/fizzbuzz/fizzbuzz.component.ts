import {Component} from '@angular/core';
import {FizzBuzzService} from '../../services/fizzbuzz.service';

@Component({
  selector: 'app-fizz-buzz',
  templateUrl: './fizzbuzz.component.html',
  styleUrls: ['./fizzbuzz.component.sass']
})
export class FizzBuzzComponent {

  results: string[] = [];
  signature = "";
  started = false;

  constructor(private service: FizzBuzzService) {
  }

  onRandomize() {
    this.service.getRandomizedResults().subscribe(res => {
      this.results = res.items;
      this.signature = "Signed at: " + this.convertToDate(res.signature).toString();
    });
  }

  convertToDate(ticks: number) : Date {
    const epochTicks = 621355968000000000, ticksPerMillisecond = 10000;
    let jsTicks = 0;

    jsTicks = (ticks - epochTicks) / ticksPerMillisecond;
    return new Date(jsTicks);
  }
}

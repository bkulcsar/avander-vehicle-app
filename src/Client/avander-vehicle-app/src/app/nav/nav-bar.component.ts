import { Component } from '@angular/core';
import { faCar } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'nav-bar',
  templateUrl: './nav-bar.component.html',
})
export class NavBarComponent {
  faCar = faCar;
}

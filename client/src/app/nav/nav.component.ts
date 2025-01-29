import { Component, inject } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive, ToastrModule, TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
    accountService = inject(AccountService);
    private route = inject(Router);
    toastr = inject(ToastrService);
    model: any = {};

    login() {
      this.accountService.login(this.model).subscribe({
        next: response => {
          console.log(response)
        },
        error: error => this.toastr.error(error.error)
      })
    }

    logout() {
      this.accountService.logout();
      this.route.navigateByUrl('/')
    }
}

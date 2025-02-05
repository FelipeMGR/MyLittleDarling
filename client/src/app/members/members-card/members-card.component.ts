import { Component, input } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-members-card',
  imports: [RouterLink],
  templateUrl: './members-card.component.html',
  styleUrl: './members-card.component.css'
})
export class MembersCardComponent {
  members = input.required<Member>()
}

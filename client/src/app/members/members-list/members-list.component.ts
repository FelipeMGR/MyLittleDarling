import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { MembersCardComponent } from "../members-card/members-card.component";

@Component({
  selector: 'app-members-list',
  imports: [MembersCardComponent],
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css'
})
export class MembersListComponent implements OnInit{
  private memberService = inject(MembersService);
  members: Member[] = []
  
  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers(){
    this.memberService.getAllMembers().subscribe({
      next: members => this.members = members
    })
  }
}

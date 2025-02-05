import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-members-detail',
  imports: [TabsModule, GalleryModule],
  templateUrl: './members-detail.component.html',
  styleUrl: './members-detail.component.css'
})
export class MembersDetailComponent implements OnInit{
  private memberService = inject(MembersService);
  private route = inject(ActivatedRoute);
  image: GalleryItem[] = [];
  member?: Member;

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers(){
    const userName = this.route.snapshot.paramMap.get('username');
    if(userName == null) return;
    this.memberService.getMember(userName).subscribe({
      next: member => {
        this.member = member
        member.photos.map(p => {
          this.image.push(new ImageItem({
            src: p.url,
            thumb: p.url
          }))
        })
      }
    })
  }
}

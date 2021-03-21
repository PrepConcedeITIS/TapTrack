import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Profile} from "./dto/profile";
import {Router} from "@angular/router";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  fileToUpload: File = null;
  userProfile: Profile;
  isNameEdit: boolean;
  isFileLoaded: boolean;
  private controllerName = "/profile";

  constructor(private httpClient: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    this.isNameEdit = false;
    this.isFileLoaded = false;
    console.log("hello");
    this.getProfile();
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.isFileLoaded = true;
    console.log("addedas");
    console.log(this.fileToUpload);
  }

  getProfile() {
    this.httpClient.get(environment.apiUrl + this.controllerName)
      .subscribe(data => {
        // @ts-ignore
        this.userProfile = data;
        console.log(data);
      });
  }

  enableUserNameEdit() {
    this.isNameEdit = true;
    console.log(this.isNameEdit);
  }

  cancelUserNameEdit() {
    this.isNameEdit = false;
    console.log(this.userProfile.userName);
    // @ts-ignore
    document.getElementById("userNameInput").value = this.userProfile.userName;
  }

  saveUserNameEdit() {
    // @ts-ignore
    const newUsername = document.getElementById("userNameInput").value;

    if (newUsername === null || typeof newUsername === "undefined" || newUsername.startsWith(' ') || newUsername.length > 20) {
      alert("Неверно введено новое имя");
      return;
    }

    this.httpClient.post(environment.apiUrl + this.controllerName + "/updateUserName", {
      newName: newUsername
    })
      .subscribe(data => {
        console.log(data);
        this.router.navigate(['login']);
      });
  }
}

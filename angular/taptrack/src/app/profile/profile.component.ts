import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Profile} from "./dto/profile";
import {Router} from "@angular/router";
import {FormGroup} from "@angular/forms";
import {FormlyFieldConfig} from "@ngx-formly/core";

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

  form = new FormGroup({});
  fields: FormlyFieldConfig[] = [
    {
      key: 'image',
      type: 'file',
      templateOptions: {
        label: 'Profile image'
      }
    }
  ];

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

  saveNewProfileImage() {
    const formData = new FormData();

    formData.set("Image", this.fileToUpload, this.fileToUpload.name);


    this.httpClient.put(environment.apiUrl + this.controllerName + "/uploadProfileImage", formData)
      .subscribe(data => {
        console.log(data);
        location.reload();
      });
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
    const newUserName = document.getElementById("userNameInput").value;

    if (newUserName === null || typeof newUserName === "undefined" || newUserName.startsWith(' ') || newUserName.length > 20) {
      alert("Неверно введено новое имя");
      return;
    }

    console.log(newUserName);

    this.httpClient.put(environment.apiUrl + this.controllerName + "/updateUserName", {
      newUserName
    })
      .subscribe(data => {
        console.log(data);
        location.reload();
      });
  }
}

import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Profile} from "./dto/profile";
import {Router} from "@angular/router";
import {FormGroup} from "@angular/forms";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {ProjectQuery} from "../project/_interfaces/project-query";
import {ContactInfo} from "./dto/contact-info";
import {UserProject} from "./dto/user-project";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  fileToUpload: File = null;
  userProfile: Profile;
  isNameEdit: boolean;
  isContactInfoEdit: boolean;
  isFileLoaded: boolean;
  isTelegramNotificationsEnabled: boolean;
  private controllerName = "/profile";

  userProjectsRowData: UserProject[];
  userContactsRowData: ContactInfo[];

  displayedColumns: string[] = ['projectName', 'userPosition'];

  form = new FormGroup({});

  constructor(private httpClient: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    console.log("helo");
    this.isNameEdit = false;
    this.isFileLoaded = false;
    this.isContactInfoEdit = false;
    this.getProfile();
    this.getUserProjects();
    this.getContactsInformation();
    //this.getNotificationOptions();

    console.log(this.userProfile.profileImageLink);
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.isFileLoaded = true;
  }

  saveNewProfileImage() {
    const formData = new FormData();

    formData.append("Image", this.fileToUpload);
    formData.append("ClaimsPrincipal",null)

    this.httpClient.post(environment.apiUrl + this.controllerName + "/uploadProfileImage", formData)
      .subscribe(data => {
        this.userProfile = <Profile> data;
      });
  }

  getProfile() {
    this.httpClient.get(environment.apiUrl + this.controllerName)
      .subscribe(data => {
        // @ts-ignore
        this.userProfile = <Profile> data;
        console.log(data);
      });
  }

  getUserProjects() {
    this.httpClient.get(environment.apiUrl + this.controllerName + "/projects")
      .subscribe(data => {
        this.userProjectsRowData = <UserProject[]> data;
      });
  }

  getContactsInformation() {
    this.httpClient.get(environment.apiUrl + this.controllerName + "/contacts")
      .subscribe(data => {

        this.userContactsRowData = <ContactInfo[]> data;
      });
  }

  getNotificationOptions() {
    this.httpClient.get(environment.apiUrl + this.controllerName + "/notificationOptions")
      .subscribe(data => {
        this.isTelegramNotificationsEnabled = <boolean> data;
      })
  }

  enableUserNameEdit() {
    this.isNameEdit = true;
  }

  enableContactInformationEdit() {
    this.isContactInfoEdit = true;
  }

  cancelUserNameEdit() {
    this.isNameEdit = false;
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

    this.httpClient.put(environment.apiUrl + this.controllerName + "/updateUserName", {
      newUserName
    })
      .subscribe(data => {
        this.userProfile = <Profile> data;
        this.isNameEdit = false;
      });
  }

  cancelContactsEdit() {
    this.isContactInfoEdit = false;
  }

  saveUserContactsEdit() {
    this.httpClient.put(environment.apiUrl + this.controllerName + "/updateContactsInfo", {
      Contacts: this.userContactsRowData,
    })
      .subscribe(data => {
        this.userContactsRowData = <ContactInfo[]> data;
        this.isContactInfoEdit = false;
      });
  }

  changeNotificationOption() {
    this.httpClient.put(environment.apiUrl + this.controllerName + "/changeNotificationOption", {
      option: this.isTelegramNotificationsEnabled
    }).subscribe(data => {
      this.isTelegramNotificationsEnabled = <boolean> data;
    })
  }
}

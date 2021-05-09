import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Profile} from "./dto/profile";
import {Router} from "@angular/router";
import {FormGroup} from "@angular/forms";
import {ContactInfo} from "./dto/contact-info";
import {UserProject} from "./dto/user-project";
import {BsModalRef, BsModalService, ModalOptions} from "ngx-bootstrap/modal";
import {TelegramBindingComponent} from "./telegram-binding/telegram-binding.component";

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
  bsModalRef: BsModalRef;
  private controllerName = "/profile";

  userProjectsRowData: UserProject[];
  userContactsRowData: ContactInfo[];

  displayedColumns: string[] = ['projectName', 'userPosition'];

  form = new FormGroup({});

  constructor(private httpClient: HttpClient,
              private router: Router,
              private modalService: BsModalService) {
  }

  ngOnInit(): void {
    this.isNameEdit = false;
    this.isFileLoaded = false;
    this.isContactInfoEdit = false;
    this.getProfile();
    this.getUserProjects();
    this.getContactsInformation();
    this.getNotificationOptions();
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.isFileLoaded = true;
  }

  saveNewProfileImage() {
    const formData = new FormData();

    formData.append("Image", this.fileToUpload);

    this.httpClient.post(environment.apiUrl + this.controllerName + "/uploadProfileImage", formData)
      .subscribe(data => {
        this.userProfile = (data as Profile);
      });
  }

  getProfile() {
    this.httpClient.get(environment.apiUrl + this.controllerName)
      .subscribe(data => {
        this.userProfile = (data as Profile);
      });
  }

  getUserProjects() {
    this.httpClient.get(environment.apiUrl + this.controllerName + "/projects")
      .subscribe(data => {
        this.userProjectsRowData = (data as UserProject[]);
      });
  }

  getContactsInformation() {
    this.httpClient.get(environment.apiUrl + this.controllerName + "/contacts")
      .subscribe(data => {

        this.userContactsRowData = (data as ContactInfo[]);
      });
  }

  getNotificationOptions() {
    this.httpClient.get(environment.apiUrl + this.controllerName + "/notificationOptions")
      .subscribe(data => {
        this.isTelegramNotificationsEnabled = (data as boolean);
      });
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
        this.userProfile = (data as Profile);
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
        this.userContactsRowData = (data as ContactInfo[]);
        this.isContactInfoEdit = false;
      });
  }

  openTelegramNotificationManagement() {
    const initialState = {
    };
    this.bsModalRef = this.modalService.show(TelegramBindingComponent, {initialState});
  }
}

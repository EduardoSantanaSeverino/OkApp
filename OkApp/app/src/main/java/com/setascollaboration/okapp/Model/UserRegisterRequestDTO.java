package com.setascollaboration.okapp.Model;

/*
* "name": "eduardo42",
  "surname": "eduardo42",
  "userName": "eduardo42",
  "emailAddress": "eduardo42@me.com",
  "password": "eduardo42@42",
  "dateOfBirth": "2020-03-25T20:38:32.400Z",
  "languageId": 1
}
*/
public class UserRegister {

    private String userName;
    private String password;
    private String emailAddress;
    private String dateOfBirth;
    private String languageId;
    private String name;
    private String surname;

    public UserRegister(){}

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getSurname() {
        return surname;
    }

    public void setSurname(String surname) {
        this.surname = surname;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getEmailAddress() {
        return emailAddress;
    }

    public void setEmailAddress(String emailAddress) {
        this.emailAddress = emailAddress;
    }

    public String getDateOfBirth() {
        return dateOfBirth;
    }

    public void setDateOfBirth(String dateOfBirth) {
        this.dateOfBirth = dateOfBirth;
    }

    public String getLanguageId() {
        return languageId;
    }

    public void setLanguageId(String languageId) {
        this.languageId = languageId;
    }

    public UserRegister(String name, String surname, String userName, String emailAddress, String password, String dateOfBirth, String languageId){
        this.name = name;
        this.surname = surname;
        this.userName = userName;
        this.emailAddress = emailAddress;
        this.password = password;
        this.dateOfBirth = dateOfBirth;
        this.languageId = languageId;
    }
}

package com.setascollaboration.okapp.Model;

public class UserRegisterRequestDTO {

    private String userName;
    private String password;
    private String emailAddress;
    private String dateOfBirth;
    private int languageId;
    private String name;
    private String surname;

    public UserRegisterRequestDTO(){}

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

    public int getLanguageId() {
        return languageId;
    }

    public void setLanguageId(int languageId) {
        this.languageId = languageId;
    }

    public UserRegisterRequestDTO(String name, String surname, String userName, String emailAddress, String password, String dateOfBirth, int languageId){

        this.name = name;
        this.surname = surname;
        this.userName = userName;
        this.emailAddress = emailAddress;
        this.password = password;
        this.languageId = languageId;

        // TODO: FIX
        // Please note the setter for date of birth is hard code.
        // Please take the dateOfBirth and validate the format is as below if not, throw exception
        // Please add some logic to generate the date format below from the user input.

        this.dateOfBirth = "2020-03-25T00:00:00.000Z"; // <-- validate it

        // END - FIX

    }
}

package com.setascollaboration.okapp.Model;

public class User {
    private String userName;
    private String password;
    private String email;
    private String birthdate;
    private String language;

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

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getBirthdate() {
        return birthdate;
    }

    public void setBirthdate(String birthdate) {
        this.birthdate = birthdate;
    }

    public String getLanguage() {
        return language;
    }

    public void setLanguage(String language) {
        this.language = language;
    }

    public User(String userName, String password){
        this.userName = userName;
        this.password = password;
    }

    public User(String userName, String password, String email, String birthdate, String language){
        this.userName = userName;
        this.password = password;
        this.email = email;
        this.birthdate = birthdate;
        this.language = language;
    }

}

package com.setascollaboration.okapp.ui.login;

import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import com.setascollaboration.okapp.Model.UserRegisterRequestDTO;
import com.setascollaboration.okapp.Model.UserRegisterResponseDTO;
import com.setascollaboration.okapp.R;
import org.springframework.http.converter.json.MappingJackson2HttpMessageConverter;
import org.springframework.web.client.RestTemplate;

import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.Date;
import java.util.Locale;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class RegisterActivity extends AppCompatActivity {

    private EditText passwordField;
    private EditText nameField;
    private EditText surnameField;
    private EditText usernameField;
    private EditText emailAddressField;
    private EditText dateOfBirthField;
    private Spinner languageIdField;
    private String ServerUrl;
    private TextView registerAccount;
    private SharedPreferences myPreference;
    private AlertDialog.Builder builder;
    private AlertDialog dialog;
    private int languageId;
    private boolean validate;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        //Assign to specific elements
        nameField         = (EditText) findViewById(R.id.txt_userName);
        surnameField      = (EditText) findViewById(R.id.txt_userName);
        usernameField     = (EditText) findViewById(R.id.txt_userName);
        passwordField     = (EditText) findViewById(R.id.txt_password);
        emailAddressField = (EditText) findViewById(R.id.txt_emailAddress);
        dateOfBirthField  = (EditText) findViewById(R.id.txt_dateOfBirth);
        languageIdField   = (Spinner) findViewById(R.id.spinner_language);
        registerAccount   = (TextView) findViewById(R.id.txtRegisterAcc);


        languageIdField.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int pos, long id) {
                switch (pos) {
                    case 0:
                        setLocale("1");
                        languageId = 1;
                        break;
                    case 1:
                        setLocale("2");
                        languageId = 6;
                        break;
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        registerAccount.setOnClickListener(new View.OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.O)
            @Override
            public void onClick(View view) {

                validate = false;
                validateFields();

                if (validate == true) {

                    DateTimeFormatter parserData = DateTimeFormatter.ofPattern("dd/MM/yyyy");
                    LocalDate data = LocalDate.parse(dateOfBirthField.getText().toString(), parserData);
                    String newFormatDateOfBirth = data+"T00:00:00.000Z";

                    UserRegisterRequestDTO c = new UserRegisterRequestDTO(nameField.getText().toString(),     surnameField.getText().toString(),
                                                                          usernameField.getText().toString(), emailAddressField.getText().toString(),
                                                                          passwordField.getText().toString(), newFormatDateOfBirth, languageId);

                    UserRegisterRequestDTO[] userRegisterArray = {c};
                    dialog.show();
                    try {
                        new RegisterActivity.RegisterRestTask().execute(userRegisterArray);
                    } catch (Exception e) {
                        Log.e("RegisterActivity", e.getMessage());
                        Toast.makeText(getApplicationContext(), getResources().getString(R.string.server_error), Toast.LENGTH_LONG).show();
                    }
                }
            }
        });

        myPreference = getSharedPreferences("MyPrefs",MODE_PRIVATE);
        ServerUrl = myPreference.getString("IPAddress", "http://ec2-99-79-47-193.ca-central-1.compute.amazonaws.com:21021/api/services/app/Account/Register");
        builder = new AlertDialog.Builder(RegisterActivity.this);
        builder.setCancelable(false); // if you want user to wait for some process to finish,
        builder.setView(R.layout.myprogress_dialog);
        dialog = builder.create();

        languageIdField = (Spinner) findViewById(R.id.spinner_language);
        Spinner spinner = (Spinner) findViewById(R.id.spinner_language);
        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this, R.array.spinner_language, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(adapter);
    }

    private class RegisterRestTask extends AsyncTask<UserRegisterRequestDTO, Void, UserRegisterResponseDTO> {

        @Override
        protected UserRegisterResponseDTO doInBackground(UserRegisterRequestDTO... users) {
            try {
                RestTemplate restTemplate = new RestTemplate();
                restTemplate.getMessageConverters().add(new MappingJackson2HttpMessageConverter());
                return restTemplate.postForObject(ServerUrl, users[0], UserRegisterResponseDTO.class);
            } catch (Exception ex) {
                Log.e("ERROR: "+ServerUrl, ex.getMessage());
                return null;
            }
        }

        @Override
        protected void onPostExecute(UserRegisterResponseDTO userRegisterDTO) {
            super.onPostExecute(userRegisterDTO);

            dialog.dismiss();
            if(userRegisterDTO != null)
            {
                SharedPreferences.Editor prefEditor = myPreference.edit();
                prefEditor.commit();

                Toast.makeText(getApplicationContext(),
                         usernameField.getText().toString()+" Registered with Success !",
                        Toast.LENGTH_LONG).show();

                Intent intent = new Intent(getApplicationContext(), LoginActivity.class);
                intent.putExtra("statuscode", 1);
                startActivity(intent);
            }
            else
            {
                AlertDialog alertDialog = new AlertDialog.Builder(RegisterActivity.this).create();
                alertDialog.setCancelable(false);
                alertDialog.setTitle(getResources().getString(R.string.alert));
                alertDialog.setMessage(getResources().getString(R.string.register_error));

                alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
                        new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {

                            }
                        });
                alertDialog.show();
            }

        }
    }

    private void setLocale(String lang) {
        Locale locale = new Locale(lang);
        Locale.setDefault(locale);
        Configuration config = new Configuration();
        config.locale = locale;
        getBaseContext().getResources().updateConfiguration(config, getBaseContext().getResources().getDisplayMetrics());
        SharedPreferences.Editor editor = getSharedPreferences("Settings", MODE_PRIVATE).edit();
        editor.putString("my_lang", lang);
        editor.apply();
    }


    //validate fields filled in page
    private void validateFields() {
        boolean res         = false;
        boolean invalidDate = false;

        String username     = usernameField.getText().toString();
        String emailAddress = emailAddressField.getText().toString();
        String password     = passwordField.getText().toString();
        String dateBirth    = dateOfBirthField.getText().toString();

        //created to validate the date format
        Date dateTmp = null;
        Date todayDate = new Date();

        String dataText = new String(dateBirth);
        SimpleDateFormat format = new SimpleDateFormat("dd/MM/yyyy");
        try {
            format.setLenient(false);
            dateTmp = format.parse(dataText);
            //date must be less than actual date
            if (dateTmp.compareTo(todayDate) == 1) {
                invalidDate = true;
            }
            } catch (Exception e) {
                invalidDate = true;
        }

        // created to validate the email pattern
        Pattern pattern;
        Matcher matcher;
        final String EMAIL_PATTERN = "^[_A-Za-z0-9-]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$";
        pattern = Pattern.compile(EMAIL_PATTERN);
        matcher = pattern.matcher(emailAddress);

        //created to validate the information inserted on the fields
         if (res = isFieldEmpty(username)) {
             usernameField.requestFocus();
        } else if (res = isFieldEmpty(emailAddress)) {
             emailAddressField.requestFocus();
        } else if (res = isFieldEmpty(password)) {
             passwordField.requestFocus();
        } else if (res = isFieldEmpty(dateBirth)) {
             dateOfBirthField.requestFocus();
        };

        //showing a dialog box on the screen when the fields are not filled or breaks the rules
        if (res) {
            AlertDialog.Builder dlg = new AlertDialog.Builder((this));
            dlg.setTitle("Warning!");
            dlg.setMessage("All fields must be filled!");
            dlg.setNeutralButton("OK", null);
            dlg.show();
            res = false;
        } else if (password.length() < 8) {
            AlertDialog.Builder dlg = new AlertDialog.Builder((this));
            dlg.setTitle("Warning!");
            dlg.setMessage("Password must be at least 8 characteres!");
            dlg.setNeutralButton("OK", null);
            dlg.show();
            res = false;
        } else if (invalidDate == true) {
            AlertDialog.Builder dlg = new AlertDialog.Builder((this));
            dlg.setTitle("Warning!");
            dlg.setMessage("Invalid Date of Birth!");
            dlg.setNeutralButton("OK", null);
            dlg.show();
            dateOfBirthField.requestFocus();
            res = false;
        } else if (!matcher.matches()){
            AlertDialog.Builder dlg = new AlertDialog.Builder((this));
            dlg.setTitle("Warning!");
            dlg.setMessage("Invalid Email!");
            dlg.setNeutralButton("OK", null);
            dlg.show();
            emailAddressField.requestFocus();
            res = false;
        } else  {
            validate = true;
        }
    }

    private boolean isFieldEmpty(String value){

        boolean result = (TextUtils.isEmpty(value) || value.trim().isEmpty());
        return result;
    }
}


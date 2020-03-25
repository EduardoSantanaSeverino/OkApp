package com.setascollaboration.okapp.ui.login;

import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import com.setascollaboration.okapp.MainActivity;
import com.setascollaboration.okapp.Model.UserRegister;
import com.setascollaboration.okapp.Model.UserRegisterDTO;
import com.setascollaboration.okapp.R;
import org.springframework.http.converter.json.MappingJackson2HttpMessageConverter;
import org.springframework.web.client.RestTemplate;
import java.util.Locale;

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
                        break;
                    case 1:
                        setLocale("2");
                        break;
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        registerAccount.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                UserRegister c = new UserRegister(nameField.getText().toString(), surnameField.getText().toString(), usernameField.getText().toString(),emailAddressField.getText().toString(), passwordField.getText().toString(),
                        dateOfBirthField.getText().toString(), "1");

                UserRegister[] userRegisterArray = {c};
                dialog.show();
                try {
                    new RegisterActivity.RegisterRestTask().execute(userRegisterArray);
                } catch (Exception e) {
                    Log.e("RegisterActivity", e.getMessage());
                    Toast.makeText(getApplicationContext(), getResources().getString(R.string.server_error), Toast.LENGTH_LONG).show();
                }
            }
        });

        myPreference = getSharedPreferences("MyPrefs",MODE_PRIVATE);
        ServerUrl = myPreference.getString("IPAddress", "http://ec2-99-79-47-193.ca-central-1.compute.amazonaws.com:21021");
        builder = new AlertDialog.Builder(RegisterActivity.this);
        builder.setCancelable(false); // if you want user to wait for some process to finish,
        builder.setView(R.layout.myprogress_dialog);
        dialog = builder.create();

        languageIdField = (Spinner) findViewById(R.id.spinner_language);
        //Populate the language dropdown
        Spinner spinner = (Spinner) findViewById(R.id.spinner_language);
        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this, R.array.spinner_language, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(adapter);
    }

    private class RegisterRestTask extends AsyncTask<UserRegister, Void, UserRegisterDTO> {

        @Override
        protected UserRegisterDTO doInBackground(UserRegister... users) {
            try {
                RestTemplate restTemplate = new RestTemplate();
                restTemplate.getMessageConverters().add(new MappingJackson2HttpMessageConverter());
                return restTemplate.postForObject("http://ec2-99-79-47-193.ca-central-1.compute.amazonaws.com:21021/api/services/app/Account/Register", users[0], UserRegisterDTO.class);
            } catch (Exception ex) {
                Log.e("ERROR: "+ServerUrl, ex.getMessage());
                return null;
            }
        }

        @Override
        protected void onPostExecute(UserRegisterDTO userRegisterDTO) {
            super.onPostExecute(userRegisterDTO);

            dialog.dismiss();
            if(userRegisterDTO != null)
            {
                SharedPreferences.Editor prefEditor = myPreference.edit();
                // prefEditor.putString("Token", userDTO.getToken());
                prefEditor.commit();
                Toast.makeText(getApplicationContext(),
                        getResources().getString(R.string.welcome)+", "+usernameField.getText().toString()+"!",
                        Toast.LENGTH_LONG).show();

                Intent intent = new Intent(getApplicationContext(), MainActivity.class);
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

}


package com.setascollaboration.okapp.ui.home;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.AutoCompleteTextView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;

import com.setascollaboration.okapp.R;

import java.util.ArrayList;
import java.util.List;

public class HomeFragment extends Fragment {

    private HomeViewModel homeViewModel;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        List<String> fruits = new ArrayList<String>();
        fruits.add("test");

        homeViewModel =
                ViewModelProviders.of(this).get(HomeViewModel.class);
        View root = inflater.inflate(R.layout.fragment_home, container, false);
        AutoCompleteTextView actv = (AutoCompleteTextView) root.findViewById(R.id.autoCompleteTextView);
        actv.setAdapter(new ArrayAdapter<String>(getActivity().getApplicationContext(),
                R.layout.custom_autocomplete , fruits));
        actv.setThreshold(1);
        return root;
    }
}

package com.spring.dto;

import java.util.UUID;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class EmployeeDto { //transfer data between client and server
    private UUID id;
    private String name;
    private String email;
    private String phone;
    private float salary;
}

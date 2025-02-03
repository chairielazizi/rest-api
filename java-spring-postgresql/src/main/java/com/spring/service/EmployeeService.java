package com.spring.service;

import java.util.UUID;

import com.spring.dto.EmployeeDto;

public interface EmployeeService {
    EmployeeDto createEmployee(EmployeeDto employeeDto);
}

package com.spring.service;

import java.util.List;
import java.util.UUID;

import com.spring.dto.EmployeeDto;

public interface EmployeeService {
    EmployeeDto createEmployee(EmployeeDto employeeDto);
    List<EmployeeDto> getAllEmployees();
    EmployeeDto getEmployeeById(UUID employeeId);
    EmployeeDto updateEmployee(UUID employeeId, EmployeeDto employeeDto);
    void deleteEmployee(UUID employeeId);
}

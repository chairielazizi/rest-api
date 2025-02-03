package com.spring.service.impl;

import java.util.UUID;

import org.springframework.stereotype.Service;

import com.spring.dto.EmployeeDto;
import com.spring.entity.Employee;
import com.spring.exception.ResourceNotFoundException;
import com.spring.mapper.EmployeeMapper;
import com.spring.repository.EmployeeRepository;
import com.spring.service.EmployeeService;

import lombok.AllArgsConstructor;

@Service
@AllArgsConstructor
public class EmployeeServiceImpl implements EmployeeService {

    private EmployeeRepository employeeRepository;

    @Override
    public EmployeeDto createEmployee(EmployeeDto employeeDto) {
        //create employee
        Employee employee = EmployeeMapper.mapToEmployee(employeeDto);
        Employee savedEmployee = employeeRepository.save(employee);
        return EmployeeMapper.mapToEmployeeDto(savedEmployee);
    }

}

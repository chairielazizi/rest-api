package com.spring.controller;

import java.util.List;
import java.util.UUID;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.spring.dto.EmployeeDto;
import com.spring.service.EmployeeService;

import lombok.AllArgsConstructor;

@AllArgsConstructor
@RestController
@RequestMapping("/api/employees")
public class EmployeeController {
    private EmployeeService employeeService;

    //create employee
    @PostMapping
    public ResponseEntity<EmployeeDto> createEmployee(@RequestBody EmployeeDto employeeDto) {
        EmployeeDto savedEmployee = employeeService.createEmployee(employeeDto);
        return new ResponseEntity<EmployeeDto>(savedEmployee, HttpStatus.CREATED);
        // return ResponseEntity.ok(employeeService.createEmployee(employeeDto));
    }

    //get employee by id
    @GetMapping("/{Id}")
    public ResponseEntity<EmployeeDto> getEmployeeById(@PathVariable("Id") UUID employeeId) {
        EmployeeDto employeeDto = employeeService.getEmployeeById(employeeId);
        return new ResponseEntity<EmployeeDto>(employeeDto, HttpStatus.OK);
    }

    //get all employees
    @GetMapping
    public ResponseEntity<List<EmployeeDto>> getAllEmployees() {
        List<EmployeeDto> employeeDtos = employeeService.getAllEmployees();
        return new ResponseEntity<List<EmployeeDto>>(employeeDtos, HttpStatus.OK);
    }

    //update employee
    @PostMapping("/{Id}")
    public ResponseEntity<EmployeeDto> updateEmployee(@PathVariable("Id") UUID employeeId,
            @RequestBody EmployeeDto employeeDto) {
        EmployeeDto updatedEmployee = employeeService.updateEmployee(employeeId, employeeDto);
        return new ResponseEntity<EmployeeDto>(updatedEmployee, HttpStatus.OK);
    }

    //delete employee
    @DeleteMapping("/{Id}")
    public ResponseEntity<String> deleteEmployee(@PathVariable("Id") UUID employeeId) {
        employeeService.deleteEmployee(employeeId);
        return new ResponseEntity<String>("Employee deleted successfully", HttpStatus.OK);
    }
}

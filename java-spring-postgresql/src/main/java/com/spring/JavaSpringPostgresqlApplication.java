package com.spring;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
// @ComponentScan(basePackages = "com.spring.controller")
public class JavaSpringPostgresqlApplication {

	public static void main(String[] args) {
		SpringApplication.run(JavaSpringPostgresqlApplication.class, args);
	}

}

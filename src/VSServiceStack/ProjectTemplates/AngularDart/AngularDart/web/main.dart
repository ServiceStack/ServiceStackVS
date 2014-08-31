library hello_servicestack_example;

import 'package:angular/angular.dart';
import 'package:angular/application_factory.dart';
import 'package:hello_servicestack/hello_servicestack.dart';

void main() {
  applicationFactory().addModule(new HelloServiceStackApp()).run();
}
library hello_servicestack;

import 'package:angular/angular.dart';
import 'dart:async';
import 'dart:html';
import 'dart:convert';

part 'services/hello_servicestack_service.dart';
part 'components/hello_servicestack_component.dart';
part 'models/hello_response.dart';

class HelloServiceStackApp extends Module {
  HelloServiceStackApp() {
    bind(HelloServiceStackComponent);
    bind(HelloService);
  }
}

@Controller(
    selector: '[hello-controller]',
    publishAs: 'ctrl')
class HelloServiceStackController {
  String helloName;
  
  
}

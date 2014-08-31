part of hello_servicestack;

class HelloResponse {
  HelloResponse(Map map) {
    this.Result = map["Result"];
  }
  String Result;
}
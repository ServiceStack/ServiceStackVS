part of hello_servicestack;

@Component(
    selector: 'hello-servicestack',
    templateUrl: 'packages/hello_servicestack/components/hello_servicestack_component.html',
    publishAs: 'cmp')
class HelloServiceStackComponent {
  
  @NgTwoWay('hello-name')
  String helloName = "";
  
  @NgOneWayOneTime('server-url')
  String serverUrl = "";
  
  String lastMessage;
  
  Scope scope;
  
  HelloService helloService;
  
  HelloServiceStackComponent(this.scope,this.helloService) {
    scope.watch("cmp.helloName",(newVal,oldVal) {
        if(newVal != null && newVal.isNotEmpty) {
          helloService.getMessage(newVal, serverUrl).then((response) {
            HelloResponse helloResponse = new HelloResponse(response.data);
            lastMessage = helloResponse.Result;
          });
        }
    });
  }
  
  void nameChanged() {
    print(helloName);
    print("testing");
  }
}
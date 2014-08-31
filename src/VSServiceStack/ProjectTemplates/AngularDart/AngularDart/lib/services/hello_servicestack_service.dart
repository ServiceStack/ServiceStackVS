part of hello_servicestack;

@Injectable()
class HelloService {
  Http http;
  HelloService(Http h) {
    this.http = h;
  }
  
  Future<HttpResponse> getMessage(String name,String url) {
    return http.get(url + "hello/" + name);
  }
}
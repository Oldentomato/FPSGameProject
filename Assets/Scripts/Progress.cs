/* namespace로 수치들을 정리하고 인터페이스로 수치저장함수만들기
   https://lab-502.tistory.com/3 여기 참조

	남은 사항   
	1. 소리추가
	2. 아래쪽에다 사격하면 캐릭터머리에 탄흔자국생기는 버그수정
	3. 무기 리스트만들고 무기변환시스템추가
	4. 파밍기능 추가
	5. 간이 맵 제작
	6. 각 총기별 애니메이션변경
	7. 서버만들기
   
fps 참고자료
https://m.blog.naver.com/PostView.nhn?blogId=choish1919&logNo=221258685616&proxyReferer=https:%2F%2Fwww.google.com%2F
네트워크 참고자료
https://m.blog.naver.com/PostView.nhn?blogId=qkrghdud0&logNo=220924595704&proxyReferer=https:%2F%2Fwww.google.com%2F

참고사항
-Vritual은 하나의 기능을 하는 완전한 클래스이며, 파생클래스에서 상속해서 추가적인 기능추가 및 virtual 한정자가 달린 것을 재정의해서

 사용가능합니다.

-Abstract는 여러개의 파생 클래스에서 공유할 기본 클래스의 공통적인 정의만 하고 ,파생클래스에서 abstract 한정자가 달린 것을

 모두 재정의(필수)해야 합니다.

-Interface에서도 abstract와 비슷하지만 멤버변수를 사용할 수 없습니다. 

 보통 abstract는 개념적으로 계층적인 구조에서 사용이 되며(동물이나 어떤 사물의 계층적인 구조가있을때) Interface는 서로다른 계층이나

 타입이라도 같은기능을 추가하고 싶을때 사용합니다.(사람이나 기계가 말을하게(speak)하는 인터페이스를 추가할때)

*/

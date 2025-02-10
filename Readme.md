![alt text](image-1.png)

![alt text](image-2.png)

## 프로젝트 설명

Unity Korea에서 주관한 Unity6 Challenge 프로젝트입니다.<br>
플레이어가 골든 김치를 찾기위해 도시를 찾아 다니는 Kimch-Run이란 제목의 러닝 액션 게임 입니다.

개발 기간 : 2025.01.16 ~ 2025.02.10

장르 : 러닝 액션

조작키 : Spacebar

게임 배포 사이트 : [Kimch-Run Challenge](https://play.unity.com/en/games/d25d79e1-c526-4fe5-9734-48bffdedf62f/kimch-run-challenge)

## 프로젝트 목표

C#, Unity의 기초를 다시 공부해보면서 감각을 되살려 재밌게 개발하는 것을 목표로 하였습니다.

### 구현 기능

- Rigidbody를 이용한 점프 기능 구현
- 배경 및 적(Enemy) 오브젝트 Scroll 기능 구현
- 적(Enemy) 및 아이템 생성, 삭제 기능 구현
- stage 변환시 플레이 배경 변화 기능 구현
- particle System을 이용한 플레이어 무적(Invincible) 효과 구현
- ObjectPool을 이용하여 오브젝트 풀링 기법 적용
- 아이템, 적(Enemy) 외곽 표현 효과 적용

### 구현할려고 했던 기능

- stage 변환시 변환 효과 적용(fade in, fade out 예정) : 효과를 적용해봤으나, 시점 변경 시 효과가 중간에 등장하여 플레이에 불편함을 느낄 수 있다고 판단하여 해당 기능을 적용하지 않았습니다.
- 아이템 점수 추가 : 아이템 점수를 추가하려 했으나, 장애물을 피하는 러닝 게임의 특성과 어울리지 않는다고 판단하였고 그대로 플레이 시간을 점수로 계산하는 방식으로 결정하였습니다.

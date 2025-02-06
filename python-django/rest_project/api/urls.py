from django.urls import path
from .views import GetAllUsersView, CreateUserView

urlpatterns = [
    path('users/', GetAllUsersView.as_view(), name='get_all_users'),
    path('users/create/', CreateUserView.as_view(), name='create_user'),
]
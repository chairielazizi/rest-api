from django.urls import path
from .views import GetAllUsersView, CreateUserView, UserDetails, user_details

urlpatterns = [
    path('users/', GetAllUsersView.as_view(), name='get_all_users'),
    path('users/create/', CreateUserView.as_view(), name='create_user'),
    # path('users/<int:pk>', UserDetails.as_view(), name='user_details'), #not sure why got assertion error when using this approach
    path('users/<int:pk>/', user_details, name='user_details'),
]
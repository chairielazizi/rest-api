from rest_framework.decorators import APIView, api_view
from rest_framework.response import Response
from rest_framework import status
from rest_framework.renderers import JSONRenderer
from .serializer import UserSerializer
from .models import User
import json

class GetAllUsersView(APIView):
    def get(self, request):
        users = User.objects.all()
        serializer = UserSerializer(users, many=True)
        return Response(serializer.data, status=status.HTTP_200_OK)

class CreateUserView(APIView):
    def post(self, request):
        serializer = UserSerializer(data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

#not sure why got assertion error when using this approach    
class UserDetails(APIView):
    def handle_request(self, request, pk):
        handlers = {
            'GET': self.get_user,
            'PUT': self.update_user,
            'PATCH': self.partial_update_user,
            'DELETE': self.delete_user
        }
        handler = handlers.get(request.method)
        if handler:
            return handler(request, pk)
        return Response(status=status.HTTP_405_METHOD_NOT_ALLOWED)

    def get_user(self, request, pk):
        user = User.objects.get(pk = pk)
        serializer = UserSerializer(user)
        return Response(serializer.data, status=status.HTTP_200_OK)

    def update_user(self, request, pk):
        user = User.objects.get(pk = pk)
        data = json.loads(request.body)
        serializer = UserSerializer(user, data=data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_200_OK)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

    def partial_update_user(self, request, pk):
        user = User.objects.get(pk = pk)
        data = json.loads(request.body)
        serializer = UserSerializer(user, data=data, partial=True)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_200_OK, content_type='application/json')
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST, content_type='application/json')

    def delete_user(self, request, pk):
        user = User.objects.get(pk = pk)
        user.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)
    
    def dispatch(self, request, *args, **kwargs):
        return self.handle_request(request, *args, **kwargs)

@api_view(['GET', 'PUT', 'PATCH', 'DELETE'])
def user_details(request, pk):
    try:
        user = User.objects.get(pk=pk)
    except User.DoesNotExist:
        return Response(status=status.HTTP_404_NOT_FOUND)
    
    if request.method == 'GET':
        serializer = UserSerializer(user)
        return Response(serializer.data, status=status.HTTP_200_OK)
    
    elif request.method == 'PUT':
        serializer = UserSerializer(user, data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_200_OK)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
    
    elif request.method == 'PATCH':
        serializer = UserSerializer(user, data=request.data, partial=True)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_200_OK)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
    
    elif request.method == 'DELETE':
        user.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)

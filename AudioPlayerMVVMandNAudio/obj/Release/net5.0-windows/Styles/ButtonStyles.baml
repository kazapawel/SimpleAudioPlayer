   M S B A M L   `   `   `  ���� S  OAudioPlayerMVVMandNAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AudioPlayerMVVMandNAudioAudioPlayerMVVMandNAudio  W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   ImageButton �� Property �� Value �� 
TargetType ��Name �� Padding ��Name �� BitmapScalingMode �� EdgeMode	 J� RenderTransformOrigin
 J� Opacity �� RelativeSource �� Path �� ScaleX �� ScaleY �� Angle �� Triggers :� RoutedEvent �� 
TargetName �� TargetProperty	 W� To g� Duration P� Property P� Value �� 
TargetName  ImageButtonSmall  RoundButton �� CornerRadius  ForegroundDarkBrush	 t� To  PlaylistItemButton  BaseEmptyButtonStyle'   !System.Windows.Shell.WindowChrome  SystemIconButton �� BasedOn  SystemIconButtonTextbox ��Name�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     0local&clr-namespace:AudioPlayerMVVMandNAudio  %�  &	        &	     &	 
    7  &	     7  &	 �    &	 �    7  &	 �    7  �� "  ��.���  � ��$ Center=���  � ��$ Center=���  � �� �#767CFA��  � �� �� " ��.R��� $ buttonBorder��
������ $ 0q�._��� $ buttonImage��$��20��$��20��$��Center=�$��Center=�$ HighQuality=�$ Aliased=�$	 .5,.5&�$	
 0.8Q�����  �� *TemplatedParent+$	 Tag�z�V� .~��� 
ImageScale��$ 1Q�$ 1Q��� ImageRotate��$ 0Q� :� $ 
MouseEnter��.[��� .���� .��W� $ buttonImage��$ Opacity� 1$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.1$ 
0:0:0:0.05D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.1$ 
0:0:0:0.05D�:� $ 
MouseLeave��.[��� .���� .��W� $ buttonImage��$ Opacity� 0.8$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1$ 	0:0:0:0.1D�W� $ ImageRotate��$ (RotateTransform.Angle)� 0$ 	0:0:0:0.1D�:� $ PreviewMouseDown��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.2$ 
0:0:0:0.05D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.2$ 	0:0:0:0.1D�:� $ PreviewMouseUp��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1$ 
0:0:0:0.05D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1$ 	0:0:0:0.1D�P�  �@J�IsMouseOver$
 True��. ��� $ buttonBorder�� ��  �� *TemplatedParent+$ 
Background� � ����  �@J�Opacity$ 1Q��� "  ��.���  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� $ buttonBorder��
������ $��0q�
���  ��$��17��$��20��$ 0q�._��� $ buttonImage��$��17��$��17��$��Center=�$��Center=�$ HighQuality=�$ Aliased=�$	 .5,.5&�$	
 0.8Q�����  �� *TemplatedParent+$	 Tag�z�V� .~��� 
ImageScale��$ 1Q�$ 1Q��� ImageRotate��$ 0Q� :� $ 
MouseEnter��.[��� .���� .��W� $ buttonImage��$ Opacity� 1$ 	0:0:0:0.1D�:� $ 
MouseLeave��.[��� .���� .��W� $ buttonImage��$ Opacity� 0.8$ 	0:0:0:0.1D�:� $ PreviewMouseDown��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.1$ 
0:0:0:0.05D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.1$ 	0:0:0:0.1D�:� $ PreviewMouseUp��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1$ 
0:0:0:0.05D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1$ 	0:0:0:0.1D��� "  ��.���  � ��$ 4q���  � ��$ Center=���  � �� �� " ��.R��� $ circleBorder��#��z��#��z��$ 90��
������ $��2q�8��  $ 9q�._��� $ buttonImage��#��z��#��z��$��Center=�$��Center=�$ HighQuality=�$ Aliased=�$	 .5,.5&�����  �� *TemplatedParent+$	 Tag�z�V� .~��� 
ImageScale��$ 1Q�$ 1Q��� ImageRotate��$ 0Q� :� $ 
MouseEnter��.[��� .���� .��t� $ circleBorder��$ BorderThickness� 2.5$ 
0:0:0:0.05D�:� $ 
MouseLeave��.[��� .���� .��t� $ circleBorder��$ BorderThickness� 2$ 
0:0:0:0.05D�:� $ PreviewMouseDown��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.2$ 
0:0:0:0.05D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.2$ 	0:0:0:0.1D�:� $ PreviewMouseUp��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.1$ 
0:0:0:0.05D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.1$ 	0:0:0:0.1D��� "  ��.���  � ��$ 4,2,0,2q���  � ��$ Center=���  � ��$ Center=���  � ��$ 0q���  � �� �� " ��.R��� $ circleBorder��#��z��#��z��$ 90��
������ $��1q�8��  $ 3q�._��� $ buttonImage��#��z��#��z��$��Center=�$��Center=�$ HighQuality=�$ Aliased=�$	 .5,.5&�����  �� *TemplatedParent+$	 Tag�z�V� .~��� 
ImageScale��$ 1Q�$ 1Q��� ImageRotate��$ 0Q��� "  ��.���   �@  IsHitTestVisibleInChrome$
 True����  � ��
 ���� ��  � ��$ 0q���  �@�UseLayoutRounding$
 True����  � �� �� " ��.R��� �� "  ��8   .���  � ��$ 0q���  �@J�Opacity$	 0.8Q���  � ��$ 30����  � ��$ 45����  � ��
 �  ����  � �� �� " ��.R��� $ ImageBorder��$ 5q�#��z��#��z��
������ ._��� $ ImageButton��$	 .5,.5&�$��Center=�$��Center=�$ HighQuality=�$	��140��$ Aliased=����� *Tag+ �� *TemplatedParent+z�V� .~��� 
ImageScale��$	 1.3Q�$	 1.3Q��� ImageRotate��$ 0Q� P�  �@J�IsMouseOver$
 True��. ��� $ ImageBorder�� ��  �� *TemplatedParent+$ 
Background� � ����  �@J�Opacity$ 1Q�:� $ 
MouseEnter��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.5$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.5$ 	0:0:0:0.1D�:� $ 
MouseLeave��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.3$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.3$ 	0:0:0:0.1D�W� $ ImageRotate��$ (RotateTransform.Angle)� 0$ 	0:0:0:0.1D��� "  ��8   .���  � ��$ 0q���  �@J�Opacity$	 0.8Q���  � ��$ 30����  � ��$ 45����  � ��
 �  ����  � �� �� " ��.R��� $ ImageBorder��$ 5q�#��z��#��z��
������ ._��� $ 
ButtonText��$��16(�
�������$��Center=�$��Center=����� *Tag+ �� *TemplatedParent+ P�  �@J�IsMouseOver$
 True��. ��� $ ImageBorder�� ��  �� *TemplatedParent+$ 
Background� � ����  �@J�Opacity$ 1Q�:� $ 
MouseEnter��.[��� .���� .��W� $ 
ButtonText��$ FontSize� 22$ 	0:0:0:0.1D�:� $ 
MouseLeave��.[��� .���� .��W� $ 
ButtonText��$ FontSize� 16$ 	0:0:0:0.1D�
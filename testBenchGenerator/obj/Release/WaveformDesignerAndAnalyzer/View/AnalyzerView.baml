   M S B A M L   `   `   `  ���� M  ItestBenchGenerator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullyAclr-namespace:testBenchGenerator.WaveformDesignerAndAnalyzer.View3testBenchGenerator.WaveformDesignerAndAnalyzer.View  F    @testBenchGenerator.WaveformDesignerAndAnalyzer.View.AnalyzerView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     SlocalAclr-namespace:testBenchGenerator.WaveformDesignerAndAnalyzer.View     R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  
�������.���   � ColumnDefinitions  �� ��  � RowDefinitions �� $��40 ��� .+��� $��0��$��0��$��10q� �� Orientation$ 
Horizontal=�.+��� $��Designer��$
��Bold"��� -    ��Name$ desInfoBlock��$��10(�$��	100,0,0,0q�$��Center=��� $��0��$��1��$��10q�$ 
Horizontal=�.+��� $��Analyzer��$
��Bold"��� -   $ anInfoBlock��$��10(�$��	100,0,0,0q�$��Center=�� -    �Name$ designer��$��0��$��1�� �� $��2* ��� $��2* �.+�� $��0��  �� $��2* ��� $��3* � �� �� .+�  OxyPlot.Wpf.Plot  -     Name$
 wfdI��$��0��$��0��$��10q�$��Stretch=�$��Stretch=�   TitleFontSize$ 12Q�
�������   Title$ I��$��1q�   Series   OxyPlot.Wpf.LineSeries  ���� *	IPoints+	   Axes	   OxyPlot.Wpf.LinearAxis    OxyPlot.Wpf.Axis
   Title$

 Time��   Position$ Bottom=�   Unit$ s��   LabelFormatter ValueAxisLabelFormatters  $
 Left=� ValueAxisLabelFormatter  -   $
 wfdQ��$��1��$��0��$��10q�$��Stretch=�$��Stretch=�$ 12Q�
�������$ Q��$��1q�     OxyPlot.Wpf.Series   Color$ DarkRed������ *	QPoints+	   $

 Time��$ Bottom=�$ s�� ValueAxisLabelFormatters  $
 Left=� ValueAxisLabelFormatter  -   $
 wfdF��$��2��$��1��$��10q�$��Stretch=�$��Stretch=�$ 12Q�
�������$ Fourier plot��$��1q�   $ DarkBlue������ *	FPoints+	   $
 	Frequency��$ Bottom=�$ Hz�� ValueAxisLabelFormatterHz  $
 Left=� ValueAxisLabelFormatter� $��1��  �� �� $��2* ��� �� $��2* � �� �� �� �� �� �� �� �� �� �� .+��� -   $ dtype��$��0��$��0��$��Waveform type��$��Center=�$
��Left=�$��10,0,5,0q��� -   $	 dfs��$��1��$��0��$��Sampling frequency [Hz]��$��Center=�$
��Left=�$��10,0,5,0q��� -	   $	 dbw��$��2��$��0��$��Bitwidth��$��Center=�$
��Left=�$��10,0,5,0q��� -
   $ dl��$��3��$��0��$��
Length [s]��$��Center=�$
��Left=�$��10,0,5,0q��� -   $
 drms��$��4��$��0��$��
RMS [dBFs]��$��Center=�$
��Left=�$��10,0,5,0q��� -   $ df��$��5��$��0��$��Frequency [Hz]��$��Center=�$
��Left=�$��10,0,5,0q��� -   $
 dfft��$��5��$��0��$��FFT size��$��Center=�$
��Left=�$��10,0,5,0q��� -   $	 dpo��$��6��$��0��$��Phase offset [°]��$��Center=�$
��Left=�$��10,0,5,0q��� -   $	 dfo��$��7��$��0��$��	Fs offset��$��Center=�$
��Left=�$��10,0,5,0q��� -   $	 dim��$��7��$��0��$��Signal magnitude��$��Center=�$
��Left=�$��10,0,5,0q��� -   $ dofdmn��$��8��$��0��$��Number of OFDM carriers��$��Center=�$
��Left=�$��10,0,5,0q��� -   $
 dmod��$��0��$��2��$��
Modulation��$��Center=�$
��Left=�$��10,0,5,0q��� -   $	 dns��$��1��$��2��$��Number of symbols��$��Center=�$
��Left=�$��10,0,5,0q��� -   $	 dos��$��2��$��2��$��Oversampling��$��Center=�$
��Left=�$��10,0,5,0q��� -   $
 ddis��$��3��$��2��$��Distance��$��Center=�$
��Left=�$��10,0,5,0q��� -   $	 dcp��$��4��$��2��$��Cyclix prefix length��$��Center=�$
��Left=�$��10,0,5,0q��� -   $ drand��$��5��$��2��$��Use random PM��$��Center=�$
��Left=�$��	10,0,50,0q��� -   $ dseed��$��6��$��2��$��Init random seed��$��Center=�$
��Left=�$��	10,0,50,0q��� -   $
 drad��$��7��$��2��$��Radix��$��Center=�$
��Left=�$��10,0,5,0q��� -   $
 ddel��$��8��$��2��$��	Delimiter��$��Center=�$
��Left=�$��10,0,5,0q��� -    ��Name$ desType��$��0��$��1��$��Center=�$��Stretch=�$
��10,0q�$��Right=�.@��� $
��Sine���� $
��Ramp���� $
��OFDM���� $��Constant���� -    ��Name$ desFs��$��1��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *Fs+�� -   $ desBitwidth��$��2��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
Bitwidth+�� -   $ 	desLength��$��3��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
LengthTime+�� -   $ desRMS��$��4��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *RMS+�� -    $ desFreq��$��5��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *Freq+�� -!   $ desFFTLength��$��5��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *	FFTLength+�� -"   $ desPhoff��$��6��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *Phoff+�� -#   $ desFsoff��$��7��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *Fsoff+�� -$   $ desInputMag��$��7�� �� 
IsReadOnly . 
�������$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
InputMag+�� -%   $ desOFDMN��$��8��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *OFDMN+�� -&   $ modType��$��0��$��3��$��Center=�$��Stretch=�$
��10,0q�$��Right=�.@��� $
��QPSK���� $��16QAM���� $��64QAM���� -'   $ desNSymbols��$��1��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
NSymbols+�� -(   $ desOS��$��2��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *OS+�� -)   $ desDistance��$��3��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
Distance+�� -*   $ desCPLength��$��4��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
CPLength+�� -+    ��Name$ desRand��$��5��$��3��$��Center=�$��Right=�$
��10,0q� d� 	IsChecked �� *IsRand+�� -,   $ desSeed��$��6��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *Seed+�� --   $ desRadix��$��7��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *	Radixes+�� -.   $ desDel��$��8��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
Delimiters+�� $��0��$��2��$��9��$��Center=�$��Center=� � ToolTip �� *ProblemDesignToolTip+ ^� 	IsEnabled �� *
CanDesignN+.+��� -/    ��Name$ design��$��0,0,5,0q�$��Design��$��80��{��� *	CanDesign+�� $��2��$��2��$��9��$��Center=�$��Center=� �� *ProblemExportToolTip+ �� *
CanExportN+.+��� -0   $ export��$��5,0,0,0q�$��Export��$��80��{��� *	CanExport+�� -1   $ splashD��$��2��$��2��$��Center=�$��Center=�$��Center=�$��Center=�$��Generating...��
�������$	��100��$��30��
���"�"�$��1q�� -2   $ analyzer��$��1��$��1�� �� $��2* ��� $��2* �.+�� $��0��  �� $��2* ��� $��3* � �� �� .+�  -3   $
 wfaI��$��0��$��0��$��10q�$��Stretch=�$��Stretch=�$ 12Q�
�������$ I��$��1q�   ���� *	IPoints+	   $

 Time��$ Bottom=�$ s�� ValueAxisLabelFormatters  $
 Left=� ValueAxisLabelFormatter  -4   $
 wfaQ��$��1��$��0��$��10q�$��Stretch=�$��Stretch=�$ 12Q�
�������$ Q��$��1q�   $ DarkRed������ *	QPoints+	   $

 Time��$ Bottom=�$ s�� ValueAxisLabelFormatters  $
 Left=� ValueAxisLabelFormatter  -5   $
 wfaF��$��2��$��1��$��10q�$��Stretch=�$��Stretch=�$ 12Q�
�������$ Fourier plot��$��1q�   $ DarkBlue������ *	FPoints+	   $
 	Frequency��$ Bottom=�$ Hz�� ValueAxisLabelFormatterHz  $
 Left=� ValueAxisLabelFormatter� $��1��  �� �� $��2* ��� �� $��2* � �� �� �� �� �� �� �� �� �� �� .+��� -6   $	 awt��$��0��$��0��$��Waveform type��$��Center=�$
��Left=�$��10,0,5,0q��� -7   $	 afs��$��1��$��0��$��Sampling frequency [Hz]��$��Center=�$
��Left=�$��10,0,5,0q��� -8   $	 abw��$��2��$��0��$��Bitwidth��$��Center=�$
��Left=�$��10,0,5,0q��� -9   $	 ali��$��3��$��0��$��Lines to ignore��$��Center=�$
��Left=�$��10,0,5,0q��� -:   $	 aos��$��4��$��0��$��Oversampling��$��Center=�$
��Left=�$��10,0,5,0q��� -;   $	 aim��$��5��$��0��$��Input magnitude��$��Center=�$
��Left=�$��10,0,5,0q��� -<   $	 ans��$��5��$��0��$��Number of symbols��$��Center=�$
��Left=�$��10,0,5,0q��� -=   $
 afft��$��6��$��0��$��FFT size��$��Center=�$
��Left=�$��10,0,5,0q��� ->   $
 ardx��$��7��$��0��$��Radix��$��Center=�$
��Left=�$��10,0,5,0q��� -?   $
 adel��$��8��$��0��$��	Delimiter��$��Center=�$
��Left=�$��10,0,5,0q��� -@   $ af��$��0��$��2��$��Estimated frequency [Hz]��$��Center=�$
��Left=�$��10,0,5,0q��� -A   $	 ato��$��0��$��2��$��Timing offset [samples]��$��Center=�$
��Left=�$��10,0,5,0q��� -B   $	 apo��$��1��$��2��$��Phase offset [°]��$��Center=�$
��Left=�$��10,0,5,0q��� -C   $
 arms��$��2��$��2��$��
RMS [dBFs]��$��Center=�$
��Left=�$��10,0,5,0q��� -D   $	 aae��$��2��$��2��$��Average EVM��$��Center=�$
��Left=�$��10,0,5,0q��� -E   $ armsfs��$��3��$��2��$��RMS error [dBFs]��$��Center=�$
��Left=�$��10,0,5,0q��� -F   $	 aas��$��3��$��2��$��Average SNR [dB]��$��Center=�$
��Left=�$��10,0,5,0q��� -G   $ armslsbs��$��4��$��2��$��RMS error [lsbs]��$��Center=�$
��Left=�$��10,0,5,0q��� -H   $	 awe��$��4��$��2��$��	Worst EVM��$��Center=�$
��Left=�$��10,0,5,0q��� -I   $ aprmse��$��5��$��2��$��Phase RMS error [°]��$��Center=�$
��Left=�$��10,0,5,0q��� -J   $	 aws��$��5��$��2��$��Worst SNR [dB]��$��Center=�$
��Left=�$��10,0,5,0q��� -K   $	 age��$��6��$��2��$��Gain error [dB]��$��Center=�$
��Left=�$��10,0,5,0q��� -L   $
 adcr��$��7��$��2��$��DC bias real [dBFs]��$��Center=�$
��Left=�$��10,0,5,0q��� -M   $
 adci��$��8��$��2��$��DC bias imag [dBFs]��$��Center=�$
��Left=�$��10,0,5,0q��� -N   $ anType��$��0��$��1��$��Right=�$��Center=�$��Stretch=�$
��10,0q�.@��� $
��Sine���� -O   $
 anFs��$��1��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *Fs+�� -P   $
 anBW��$��2��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
Bitwidth+�� -Q   $
 anLI��$��3��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *LinesIgnore+�� -R   $
 anOS��$��4��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *OS+�� -S   $ 
anInputMag��$��5��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
InputMag+�� -T   $
 anNS��$��5��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
NSymbols+�� -U   $ anFFT��$��6��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *	FFTLength+�� -V   $ anRadix��$��7��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *	Radixes+�� -W   $ anDel��$��8��$��1��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
Delimiters+�� -X   $ anFreq�� . 
�������$��0��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *Freq+ �� StringFormat$  {0:0.####################}���� -Y   $ anToff�� . 
�������$��0��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *Toff+$  {0:0.####################}���� -Z   $ anPhoff�� . 
�������$��1��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *Phoff+$  {0:0.####################}���� -[   $ anRMS�� . 
�������$��2��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *RMS+$  {0:0.####################}���� -\   $ anAvgEVM�� . 
�������$��2��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *AvgEVM+$  {0:0.####################}���� -]   $ anRMSEFs�� . 
�������$��3��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *RMSEFs+$  {0:0.####################}���� -^   $ anAvgSNR�� . 
�������$��3��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *AvgSNR+$  {0:0.####################}���� -_   $ 
anRMSElsbs�� . 
�������$��4��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *
RMSElsbs+$  {0:0.####################}���� -`   $ anWEVM�� . 
�������$��4��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *WEVM+$  {0:0.####################}���� -a   $ anPRMSE�� . 
�������$��5��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *PRMSE+$  {0:0.####################}���� -b   $ anWSNR�� . 
�������$��5��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *WSNR+$  {0:0.####################}���� -c   $ anGainE�� . 
�������$��6��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *GainE+$  {0:0.####################}���� -d   $ anDCReal�� . 
�������$��7��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *DCReal+$  {0:0.####################}���� -e   $ anDCImag�� . 
�������$��8��$��3��$��Center=�$��Stretch=�$��Right=�$
��10,0q����� *DCImag+$  {0:0.####################}���� $��9��$��4��$��Center=�$��Center=� �� *ProblemToolTip+ �� *
CanImportN+.+��� -f   $ import��$��Import��$��80��$��Center=�$��Center=�{��� *	CanImport+�� -g   $ splashA��$��2��$��2��$��Center=�$��Center=�$��Center=�$��Center=�$��
Reading...��
�������$	��100��$��30��
���"�"�$��1q��� -h   $ splashAA��$��2��$��2��$��Center=�$��Center=�$��Center=�$��Center=�$��Analyzing...��
�������$	��100��$��30��
���"�"�$��1q��� $��2��$��0��$��0,0,1,0q�$��Stretch=�$��Stretch=�
���   �
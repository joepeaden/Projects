# This code is a heavily edited version of sample code that was found to give us a head start on the project.
# The code is for a Raspberry Pi robot, with which we use OpenCV, a motorcontroller, and a servo in order to
# find a ball of a certain color, approach it, and retrieve it.

# Motor serial information:
# MOTOR 1: 1 reverse, 64 stop, 127 forward
# MOTOR 2: 128 reverse, 192 stop, 255 forward

# Screen is divided into 3 sections, motors are directed depending
# on which section of screen the ball is in

# SAMUSBOT

import cv2
import serial
import numpy as np
import RPi.GPIO as gpio
import time

##### Servo setup #####
# For AR-3600HB Robot Servo
gpio.setmode(gpio.BOARD)
gpio.setwarnings(False)
gpio.setup(12, gpio.OUT) # pin 12 used 
##### PWM setup #####
p = gpio.PWM(12, 50) # setting pin to pulse-width modulation with frequency of 50 hertz 
p.start(7.5) # sets duty cycle to 7.5 (neutral)

def empty(z):
    pass
##### Functions #####

def turnLeft():
    #print("Ball to the left")
    ser.write(chr(int('255'))) #motor2 max forward
    ser.write(chr(int('1')))   #motor1 max reverse
def turnRight():
    #print("Ball to the right")
    ser.write(chr(int('127'))) #motor1 max forward
    ser.write(chr(int('128'))) #motor2 max reverse 
def forward():
    #print("Ball is in center")
    ser.write(chr(int('127'))) #motor1 max forward
    ser.write(chr(int('255'))) #motor2 max forward
def search():
    #print("SEARCHING")
    ser.write(chr(int('127'))) #motor2 max forward
    ser.write(chr(int('128'))) #motor2 max backward
def closeClaw():  
    ser.write(chr(int('0'))) # stop motors
    p.ChangeDutyCycle(2.5) # set servo to 0 degrees (closed position for claw)
    #print("Closing claws")
    clawOpen = 0 #false
    time.sleep(2) # pause for 2 seconds
    return clawOpen
def openClaw():
    p.ChangeDutyCycle(7.5) # sets servo to 180 degrees (open position for claw)
    #print("Opening claws")
    clawOpen = 1 #true
    return clawOpen

##### Variables #####    

height = 120
width = 160
# third and twothird separate screen into 3 sections
third = width/3
twothird = third + third
minArea = 1 # minimum area to track object
criticalArea = 13000 # area to close claws (may be too large)
clawOpen = 0 # 0 is false, 1 is true, shows if claw is open

##### Camera/serial setup #####

ser = serial.Serial('/dev/ttyS0',9600, timeout=1)
cam = cv2.VideoCapture(0)
if cam.isOpened():
    cam.set(cv2.cv.CV_CAP_PROP_FRAME_HEIGHT,120) # Setting camera height and width
    cam.set(cv2.cv.CV_CAP_PROP_FRAME_WIDTH,160) # in order to work with variables

# Begin operation loop    
openClaw()
while (True):
  

##### Setting up image tracking #####

    # tennis ball (neon green): min: (30, 233, 125), max: (192, 255, 255)
    # big ball (pink): min: (121, 39, 98), max: (240, 255, 255)
    # taped ball (neon orange): min: (0,71,179), max: (11, 255, 255)
    # dark green taylormade ball: min: (57,137,54), max: (73,255,255)

    ret,frame = cam.read()
    hsv=cv2.cvtColor(frame,cv2.COLOR_BGR2HSV)
    image_mask=cv2.inRange(hsv,np.array([121,39,98]),np.array([240,255,255]))   
    erode=cv2.erode(image_mask,None,iterations=3)
    moments=cv2.moments(erode,True)
    area=moments['m00']

##### Movement decision making #####

        
    if moments['m00'] >=minArea:
        x=moments['m10']/moments['m00']
        y=moments['m01']/moments['m00']
        cv2.circle(frame,(int(x),int(y)),5,(0,255,0),-1)
        # if area is large enough, stop, close claw
        if (area >= criticalArea):
  	    if(clawOpen == 1):
                clawOpen = closeClaw()                
        else:	
	    if (clawOpen == 0):
	 	clawOpen = openClaw()
            # if ball on right third of camera
            if (x>twothird):
             	turnRight()
            # if ball on left third of camera
            elif (x<third):
                turnLeft()
            # if ball is in center section of camera
            else:
                forward()

    # if ball is out of camera view    
    if (area<minArea ):
        search()

   # VID WINDOWS DISABLED : Won't run on startup without disabled windows
   # Shows two video windows    
   # cv2.imshow('eroded',erode)
   # cv2.imshow('frame',frame)
   # if cv2.waitKey(1)==27:
   #     break


p.stop # stopping PWM
gpio.cleanup() # resets GPIO pin
#cv2.destroyAllWindow() # not needed since vid windows are disabled
cam.release()

//
//  main.m
//  AoC2021
//
//  Created by Nathan McCrina on 12/2/21.
//

#import <Foundation/Foundation.h>

int main(int argc, const char * argv[]) {
    @autoreleasepool {
        NSFileHandle *fin;
        NSData *fileContents;
        NSString *input;
        NSArray<NSString*> *depths;
        
        fin = [NSFileHandle fileHandleForReadingAtPath: @"input_data/day1.txt"];
        fileContents = [fin readDataToEndOfFile];
        input = [[NSString alloc] initWithData:fileContents encoding:NSUTF8StringEncoding];
        depths = [input componentsSeparatedByString:@"\n"];
        int increaseCount = 0;
        int previousDepth = 0;
        
        for (int i = 0; i < [depths count]; i++)
        {
            int depth = [depths[i] intValue];
            
            if (i != 0 && depth > previousDepth)
            {
                increaseCount++;
            }
            
            previousDepth = depth;
        }
        
        NSLog(@"%d", increaseCount);
        
    }
    return 0;
}
